using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

namespace CardEffects
{
    public class BurnCardsOfType : CardEffect
    {
        public Card CardToCheck;
        private List<Card> _cardsToBurn = new List<Card>();

        public override void Invoke(Participant target)
        {
            foreach (var card in Player._hand.Cards)
            {
                if (card != ThisCard && card.name.Contains(CardToCheck.name))
                {
                    _cardsToBurn.Add(card);
                }
            }
            foreach (var card in _cardsToBurn)
            {
                Burn(card);
            }
            _cardsToBurn.Clear();
        }
    }
}