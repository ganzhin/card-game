using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class TakeCardFromDeck : CardEffect
    {
        public override void Invoke(Participant target)
        { TakeCard(); }

        public void TakeCard()
        {
            Player.TakeCardFromDeck(false);
        }
    }
}