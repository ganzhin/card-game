using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ObjectButton : MonoBehaviour
{
    public UnityEvent onClick;
    [SerializeField] private TextMesh _text;

    private void OnMouseDown()
    {
        onClick.Invoke();
    }

    private void Start()
    {
        OnMouseExit();
    }

    private IEnumerator OnMouseEnter()
    {
        while (_text != null && _text.color != Color.white)
        {
            _text.color = Color.Lerp(_text.color, Color.white, .2f);
            yield return null;
        }

    }
    private void OnMouseExit()
    {
        StopCoroutine(nameof(OnMouseEnter));
        if (_text)
            _text.color = Color.Lerp(Color.clear, Color.white, .6f);
    }
}