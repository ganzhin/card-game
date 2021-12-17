using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class AddPrice : CardEffect
    {
        public override void Invoke(Participant target)
        {
            Board.ChangeCurrentPrice(Value);
        }
    }
}