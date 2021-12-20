using UnityEngine;

namespace CardEffects
{
    public class TakeCardFromDeck : CardEffect
    {
        public int Amount = 1;

        public override void Invoke(Participant target)
        {
            for (int i = 0; i < Amount; i++)
            {
                TakeCard();
            }
        }

        public void TakeCard()
        {
            Player.TakeCardFromDeck(false);
        }
    }
}