using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class AddCardInDeck : CardEffect
    {
        public Card CardPrefab;
        public int Amount = 1;

        public override void Invoke(Participant target)
        {
            for (int i = 0; i < Amount; i++)
            {
                AddCard();
            }
        
        }

        public virtual void AddCard()
        {
            Player.InstantiateCardInDeck(CardPrefab);
        }
    }
}