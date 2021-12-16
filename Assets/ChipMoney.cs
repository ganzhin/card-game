using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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

    private List<GameObject> _chips = new List<GameObject>();

    private string text => $"Это оставшиеся фишки. \nИх тут {_money}...{((Money > 0) ? "\nДвойной клик, чтобы восстановить здоровье." : "")}";
    [SerializeField] private Dialogue _dialogue;

    private float _timeToDoubleClick = .5f;
    private bool _click;
    private float _timer;

    private void Start()
    {
        Load();
        StartCoroutine(SpawnChipsRoutine());

    }

    private void OnMouseDown()
    {
        if (_click)
        {
            ExchangeChips();
            _click = false;
        }

        _click = true;
        _timer = _timeToDoubleClick;
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
            chip.transform.SetParent(transform);
            chip.transform.position = transform.position + Vector3.up * _spawnHeight + Random.insideUnitSphere * _randomPositionModifier;
            chip.transform.eulerAngles = Random.insideUnitSphere * 360;
            yield return new WaitForSeconds(.1f);

            _chips.Add(chip);
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
        
        for (int i = _chips.Count-1; i >= 0; i--)
        {
            if (i > Money-1)
            {
                Destroy(_chips[i]);
                _chips.RemoveAt(i);
            }
        }

    }
}