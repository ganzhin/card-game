using UnityEngine;

public class CursorVisual : MonoBehaviour
{
    [SerializeField] private float _lightHeight;
    [SerializeField] private float _followSpeed;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            transform.position =
            Vector3.Lerp(
                transform.position,
                hit.point + Vector3.up * _lightHeight,
                Time.fixedDeltaTime * _followSpeed
                );
        }
    }
}
