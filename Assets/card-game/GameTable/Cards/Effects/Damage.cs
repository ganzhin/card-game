using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class Damage : CardEffect
    {
        public override void Invoke(Participant target)
        {
            target.TakeDamage(Value);
        }
    }
}