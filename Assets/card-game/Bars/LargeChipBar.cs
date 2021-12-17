using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeChipBar : Bar
{
    [SerializeField] private TextMesh _text;
    [SerializeField] private TextMesh _text2;

    public List<ChipBar> chipBars = new List<ChipBar>();
    private int fullBars = 0;

    private void Start()
    {
        if (_text)
        {
            _text.color = Color.clear;
        }
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
            _text.color = Color.clear;
    }

    public override void SetValue(int value)
    {
        if (_text)
        {
            _text.text = $"{value}";
        }
        if (_text2)
        {
            _text2.text = $"{value}";
        }

        fullBars = 0;
        foreach (var chipBar in chipBars)
        {
            chipBar.SetValue(0);
        }

        for (int i = 0; i <= value; i++)
        {
            chipBars[fullBars].SetValue(i);

            if (i >= chipBars[fullBars].GetCapacity())
            {
                fullBars++;
                i -= chipBars[fullBars].GetCapacity();
                value -= chipBars[fullBars].GetCapacity();
            }
        }
    }
}
