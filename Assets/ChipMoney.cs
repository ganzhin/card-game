using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipMoney : MonoBehaviour
{
    public static int Money
    {
        get => _money;
        set
        {
            _money = Mathf.Clamp(value, 0, 128);
            Save();
        }
    }
    public static int Floor
    {
        get => _floor;
        set
        {
            _floor = value;
            Save();
        }
    }
    public static int Health { 
        get => _health; 
        set
        {
            _health = value;
            Save();
        }
    }
    public static int MaxHealth { 
        get => _maxHealth; 
        set
        {
            _maxHealth = value;
            Save();
        }
    }

    private static int _money;
    private static int _floor;
    private static int _health;
    private static int _maxHealth;

    [SerializeField] private GameObject _chipPrefab;
    [SerializeField] private float _randomPositionModifier = 0.02f;
    [SerializeField] private float _spawnHeight;

    [SerializeField] private TextMesh _text;

    private List<GameObject> _chips = new List<GameObject>();

    private string text => $"Это оставшиеся фишки. \nИх тут {_money}...{((_healEnabled && Money > 0) ? "\nДвойной клик, чтобы восстановить здоровье." : "")}";
    [SerializeField] private Dialogue _dialogue;

    private float _timeToDoubleClick = .5f;
    private bool _click;
    private float _timer;
    [SerializeField] private bool _healEnabled = true;
    [SerializeField] private AudioClip _chipStack;

    private void Start()
    {
        Load();
        StartCoroutine(SpawnChipsRoutine());
        OnMouseExit();
    }

    private void OnMouseDown()
    {
        if (_click && _healEnabled)
        {
            ExchangeChips();
            _click = false;
        }

        _click = true;
        _timer = _timeToDoubleClick;
    }

    private IEnumerator OnMouseEnter()
    {
        while (_text != null && _text.color != Color.white)
        {
            _text.transform.localScale = Vector3.Lerp(_text.transform.localScale, Vector3.one, .1f);
            _text.color = Color.Lerp(_text.color, Color.white, .1f);
            yield return null;
        }
        UpdateMoney();
    }
    private void OnMouseExit()
    {
        StopCoroutine(nameof(OnMouseEnter));

        if (_text)
        {
            _text.color = Color.Lerp(Color.clear, Color.white, .6f);
            _text.transform.localScale = Vector3.one * .85f;
        }
    }

    private void Update()
    {
        if (_click)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _click = false;
                _dialogue.gameObject.SetActive(true);
                _dialogue.ShowString(text);
            }
        }
    }

    private IEnumerator SpawnChipsRoutine()
    {
        for (int i = 0; i < Money; i++)
        {
            var chip = Instantiate(_chipPrefab);
            chip.GetComponent<Collider>().enabled = false;
            chip.GetComponent<Rigidbody>().isKinematic = true;
            _chips.Add(chip);
        }
        for (int i = 0; i < _chips.Count; i++)
        {
            var chip = _chips[i];
            chip.transform.SetParent(transform);
            chip.transform.position = transform.position + Vector3.up * _spawnHeight + Random.insideUnitSphere * _randomPositionModifier;
            chip.transform.eulerAngles = Random.insideUnitSphere * 360;
            chip.GetComponent<Rigidbody>().isKinematic = false;
            chip.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(.02f);
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(nameof(Money), Money);
        PlayerPrefs.SetInt(nameof(Floor), Floor);
        PlayerPrefs.SetInt(nameof(Health), Health);
        PlayerPrefs.SetInt(nameof(MaxHealth), MaxHealth);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        if (PlayerPrefs.HasKey(nameof(Money)))
            _money = PlayerPrefs.GetInt(nameof(Money));

        if (PlayerPrefs.HasKey(nameof(Floor)))
            _floor = PlayerPrefs.GetInt(nameof(Floor));

        if (PlayerPrefs.HasKey(nameof(Health)))
            _health = PlayerPrefs.GetInt(nameof(Health));

        if (PlayerPrefs.HasKey(nameof(MaxHealth)))
            _maxHealth = PlayerPrefs.GetInt(nameof(MaxHealth));

        Save();
    }

    public static void Clear()
    {
        Money = 0;
        Floor = 0;
        Health = 16;
        MaxHealth = 16;
    }

    public void ExchangeChips()
    {
        if (Health == MaxHealth) return;

        if (Money >= MaxHealth - Health)
        {
            Money -= MaxHealth - Health;
            Health = MaxHealth;
        }
        else
        {
            Health += Money;
            Money = 0;
        }
        FindObjectOfType<Map>().UpdateBar();

        UpdateMoney();
        SoundDesign.PlayOneShot(_chipStack, transform);
    }

    public void UpdateMoney()
    {
        for (int i = _chips.Count - 1; i >= 0; i--)
        {
            if (i > Money - 1)
            {
                _chips[i].transform.position += Vector3.down;
                _chips[i].GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
}