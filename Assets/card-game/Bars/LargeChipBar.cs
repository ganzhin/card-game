using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeChipBar : Bar
{
    [SerializeField] private TextMesh _text;

    public List<ChipBar> chipBars = new List<ChipBar>();
    private int fullBars = 0;

    public override void SetValue(int value)
    {
        if (_text)
        {
            _text.text = $"{value}";
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
