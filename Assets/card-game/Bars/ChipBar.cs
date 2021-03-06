using UnityEngine;

public class ChipBar : Bar
{
    [SerializeField] private TextMesh _text;

    private Transform[] _valueTransforms;
    private Vector3[] _valuePositions;

    private void Start()
    {
        _valuePositions = new Vector3[transform.childCount];
        _valueTransforms = new Transform[transform.childCount];

        for (int i = 0; i < _valuePositions.Length; i++)
        {
            _valueTransforms[i] = transform.GetChild(i);
            _valuePositions[i] = _valueTransforms[i].position;
            _valueTransforms[i].position += Vector3.up;
        }

    }
    void LateUpdate()
    {
        if (_value <= _valueTransforms.Length && _value >= 0)
        {
            for (int i = 0; i < _valueTransforms.Length; i++)
            {
                if (i < _value)
                {
                    _valueTransforms[i].position = Vector3.Lerp(_valueTransforms[i].position, _valuePositions[i], Time.deltaTime * 2);
                }
                else
                {
                    _valueTransforms[i].position = Vector3.Lerp(_valueTransforms[i].position, _valuePositions[i] + Vector3.up, Time.deltaTime);

                }
            }
        }
    }
    public override void SetValue(int value)
    {
        _value = value;
        if (_text)
        {
            _text.text = $"{value}";
        }
    }
    public int GetCapacity()
    {
        return _valueTransforms.Length;
    }
    public int GetValue()
    {
        return _value;
    }

}