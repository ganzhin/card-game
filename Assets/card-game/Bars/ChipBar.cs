using System.Threading;
using UnityEngine;

public class ChipBar : Bar
{
    [SerializeField] private int _value;
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
        }

    }

    void LateUpdate()
    {
        if (_value < _valueTransforms.Length && _value >= 0)
        {
            for (int i = 0; i < _valueTransforms.Length; i++)
            {
                if (i < _value)
                {
                    _valueTransforms[i].position = Vector3.Lerp(_valueTransforms[i].position, _valuePositions[i], Time.fixedDeltaTime * 2);
                }
                else
                {
                    _valueTransforms[i].position = Vector3.Lerp(_valueTransforms[i].position, _valuePositions[i] + Vector3.up, Time.fixedDeltaTime);

                }
            }
        }
    }
    
    public override void SetValue(int value)
    {
        _value = value;
        _text.text = value.ToString();
    }
}