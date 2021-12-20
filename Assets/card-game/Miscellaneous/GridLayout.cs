using UnityEngine;

[ExecuteInEditMode]
public class GridLayout : MonoBehaviour
{
    [SerializeField] private Vector3 _spacing;
    [SerializeField] private int _columns;

    public void Update()
    {
        int column = 0;
        int row = 0;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.localPosition = column * _spacing.x * Vector3.right + row  * _spacing.y * Vector3.up;
                if (child.gameObject.activeSelf)
                {
                    column++;
                }
                if (column >= _columns)
                {
                    column = 0;
                    row++;
                }
            }
        }
    }
}