using UnityEngine;

public class Mark : MonoBehaviour
{
    const int ReadyValue = 4;

    private bool _ready;

    [SerializeField] private Texture[] _sprites;
    [SerializeField] private int _value = 0;

    private void Start()
    {
        AddMark();
    }

    public void AddMark()
    {
        _value++;
        if (_value == ReadyValue)
        {
            _ready = true;
        }
        GetComponent<Renderer>().material.mainTexture = _sprites[_value-1];
    }
}