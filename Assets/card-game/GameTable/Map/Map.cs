using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private LocationCard[] _locationCards;

    [SerializeField] private SceneAsset[] _scenes;

    [SerializeField] private int _successPercent;
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
            if (ChipMoney.Floor == 0)
                locationType = LocationType.Battle;

            bool success = random <= _successPercent;
            SceneAsset scene = success ? _scenes[(int)locationType] : _scenes[0];

            if (locationType == LocationType.Battle) success = false;

            card.Initialize(locationType, success, scene);

        }

        UpdateBar();
    }

    public void UpdateBar()
    {
        _floorText.text = $"Floor {ChipMoney.Floor}";
        _playerHealth.SetValue(ChipMoney.Health);
        _playerHealthText.text = $"{ChipMoney.Health} / {ChipMoney.MaxHealth}";
    }
}