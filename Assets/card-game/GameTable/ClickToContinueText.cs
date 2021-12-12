using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickToContinueText : MonoBehaviour
{
    [SerializeField] private string[] _strings;
    [SerializeField] private Text _textObject;

    private void OnEnable()
    {
        _textObject.text = _strings[Random.Range(0, _strings.Length)] + "\n<size=16>*click to continue*</size>";
    }
}