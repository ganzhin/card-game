using UnityEngine;

public class ShieldsEffect : CardEffect
{
    public override void Play(Card thisCard, int value)
    {
        Drop(thisCard);
    }

    public override void Play(int value, Participant target)
    {
        var participants = Object.FindObjectsOfType<Participant>();
        foreach (var participant in participants)
        {
            if (participant != target)
            {
                participant.AddArmor(value);
            }
        }
    }
}
