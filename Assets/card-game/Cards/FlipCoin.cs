using UnityEngine;

public class FlipCoin : MonoBehaviour
{
    public bool Result;
    [SerializeField] private float flipForce;
    [SerializeField] private float flipTorque;

    void Start()
    {
        Flip();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) Flip();
    }

    public int Flip()
    {

        return 0;
    }

}
