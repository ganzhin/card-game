using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private LocationCard[] _locationCards;

    [SerializeField] private SceneAsset[] _scenes;

    private void Start()
    {
        foreach (var card in _locationCards)
        {
            card.Initialize(Random.Range(0, 101) < 66, _scenes[Random.Range(0, _scenes.Length)]);
            
        }
    }
}