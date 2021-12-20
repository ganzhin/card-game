using System.Collections.Generic;
using UnityEngine;

namespace CardEffects
{
    public class BurnWithLowerPrice : CardEffect
    {
        public int Price;
        public CardEffect[] BuffEffects;

        private List<Card> _cardsToBurn = new List<Card>();

        public override void Invoke(Participant target)
        { 
            foreach (var card in Player._hand.Cards)
            {
                if (card.GetValue() <= Price)
                {
                    _cardsToBurn.Add(card);

                    if (BuffEffects.Length > 0)
                    {
                        foreach (var effect in BuffEffects)
                        {
                            effect.Invoke(target);
                        }
                    }
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