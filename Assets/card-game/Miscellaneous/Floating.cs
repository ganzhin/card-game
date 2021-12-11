using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField] private float _power = 2;

    private void Update()
    {
        transform.localPosition += Mathf.Sin(Time.time) * Vector3.up * .00001f * _power;
    }
}
