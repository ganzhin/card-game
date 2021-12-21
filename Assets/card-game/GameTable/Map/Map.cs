using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private LocationCard[] _locationCards;

    [SerializeField] private string[] _scenes;

    [SerializeField] private int _successPercent;
    [SerializeField] private int _shopSuccessPercent;
    [SerializeField] private TextMesh _floorText;

    [SerializeField] private Bar _playerHealth;
    [SerializeField] private TextMesh _playerHealthText;

    private void Start()
    {
        ChipMoney.Load();
        foreach (var card in _locationCards)
        {
            int random = Random.Range(0, 101);

            LocationType locationType = (LocationType)Random.Range(0, System.Enum.GetNames(typeof(LocationType)).Length);

            if (ChipMoney.Floor == 0 || ChipMoney.Floor % 15f == 0)
                locationType = LocationType.Battle;

            bool success;

            if (locationType == LocationType.Shop)
            {
                success = random <= _shopSuccessPercent;

            }
            else
            {
                success = random <= _successPercent;

            }

            string scene = success ? _scenes[(int)locationType] : _scenes[0];

            if (locationType == LocationType.Battle) success = false;

            card.Initialize(locationType, success, scene);

        }

        UpdateBar();

        if (ChipMoney.Floor == 46)
        {
            SceneLoader.LoadScene("UnlocksScene");
        }
    }

    public void UpdateBar()
    {
        _floorText.text = $"Floor {ChipMoney.Floor}";
        _playerHealth.SetValue(ChipMoney.Health);
        _playerHealthText.text = $"{ChipMoney.Health} / {ChipMoney.MaxHealth}";
    }
}