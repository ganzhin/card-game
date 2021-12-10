using System.Collections;
using UnityEngine;

public class Slider3D : Bar
{
    [SerializeField] private float _value;
    [SerializeField] private float _maxValue;

    [SerializeField] private TextMesh _text;
    [SerializeField] private Transform _barTransform;
    [SerializeField] private Vector3 _minSize;
    [SerializeField] private Vector3 _maxSize;
    [SerializeField] private Vector3 _minPosition;
    [SerializeField] private Vector3 _maxPosition;

    public override void SetValue(int value)
    {
        _value = value;
        _barTransform.localPosition = Vector3.Lerp(_minPosition, _maxPosition, _value / _maxValue);
        _barTransform.localScale = Vector3.Lerp(_minSize, _maxSize, _value / _maxValue);
        _barTransform.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, _value / _maxValue);
        _barTransform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(_value/2f, .5f);
        _text.text = $"{_value}\t/ \t{_maxValue}";
    }
}