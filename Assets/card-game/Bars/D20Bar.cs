using UnityEngine;

public class D20Bar : Bar
{
    [SerializeField] private Vector3[] _valueAngles;

    [SerializeField] private float _valueShowTime = .5f;

    private float _timer = 0;
    private Quaternion _oldRotation;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        transform.position += Vector3.up;
    }

    void Update()
    {
        if (_value < 21 && _value > 0)
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition, Time.fixedDeltaTime);

            _timer += Time.deltaTime;

            transform.rotation = 
                Quaternion.Lerp(
                _oldRotation,
                Quaternion.Euler(_valueAngles[_value - 1]),
                _timer/_valueShowTime
                );

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition + Vector3.up, Time.fixedDeltaTime);
        }
    }
    
    public override void SetValue(int value)
    {
        _timer = 0;
        _oldRotation = transform.rotation;
        _value = Mathf.Clamp(value, 0, 21);
    }
}