using UnityEngine;

namespace CardEffects
{
    public class CheckOtherCard : CardEffect
    {
        public Card CardToCheck;
        public CardEffect[] TrueEffects;
        
        public override void Invoke(Participant target)
        {
            foreach (var card in Board.board.Cards)
            {
                if (card != ThisCard && card.name == CardToCheck.name)
                {
                    foreach (var effect in TrueEffects)
                    {
                        effect.Invoke(target);
                    }
                    break;
                }
            }
        }
    }
}