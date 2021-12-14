using System.Collections;
using UnityEngine;

public class ChipMoney : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private GameObject _chipPrefab;
    [SerializeField] private float _randomPositionModifier = 0.02f;
    [SerializeField] private float _spawnHeight;

    private void Start()
    {
        StartCoroutine(SpawnChipsRoutine());
    }
    private IEnumerator SpawnChipsRoutine()
    {
        for (int i = 0; i < _amount; i++)
        {
            var chip = Instantiate(_chipPrefab);
            chip.transform.SetParent(transform);
            chip.transform.position = transform.position + Vector3.up * _spawnHeight + Random.insideUnitSphere * _randomPositionModifier;
            chip.transform.eulerAngles = Random.insideUnitSphere * 360;
            yield return new WaitForSeconds(.12f);
        }
    }
}