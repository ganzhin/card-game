using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class Burn : CardEffect
    {
        public override void Invoke(Participant target)
        { Burn(ThisCard); }
    }
}