using UnityEngine;

public class PotionsEffect : CardEffect
{
    public override void Play(Card thisCard, int value)
    {
        Burn(thisCard);
    }

    public override void Play(int value, Participant target)
    {
        var participants = Object.FindObjectsOfType<Participant>();
        foreach (var participant in participants)
        {
            if (participant != target)
            {
                participant.Heal(value);
            }
        }
    }
}
