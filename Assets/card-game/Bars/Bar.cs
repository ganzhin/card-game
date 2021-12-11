using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] internal int _value;

    public abstract void SetValue(int value);
}