using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class SubtractPrice : CardEffect
    {
        public override void Invoke(Participant target)
        {
            Board.ChangeCurrentPrice(-Value);
        }
    }
}