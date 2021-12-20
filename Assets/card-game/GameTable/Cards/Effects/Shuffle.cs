using UnityEngine;

namespace CardEffects
{
    public class Shuffle : CardEffect
    {
        public override void Invoke(Participant target)
        { 
            ShuffleDeck();
        }
    }
}