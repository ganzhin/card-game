using System.Threading;
using UnityEngine;

public class D20Bar : Bar
{
    [SerializeField] private int _value;
    [SerializeField] private Vector3[] _valueAngles;

    [SerializeField] private float _valueShowTime = .5f;

    private float _timer = 0;
    private Quaternion _oldRotation;

    void Update()
    {
        if (_value < 21 && _value > 0)
        {
            _timer += Time.deltaTime;

            transform.rotation = 
                Quaternion.Lerp(
                _oldRotation,
                Quaternion.Euler(_valueAngles[_value - 1]),
                _timer/_valueShowTime
                );

        }
    }
    
    public override void SetValue(int value)
    {
        _timer = 0;
        _oldRotation = transform.rotation;
        _value = Mathf.Clamp(value, 1, 20);
    }
}