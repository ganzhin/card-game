using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class Drop : CardEffect
    {
        public override void Invoke(Participant target)
        { Drop(ThisCard); }
    }
}