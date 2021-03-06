using UnityEngine;

namespace CardEffects
{
    public class Damage : CardEffect
    {
        public override void Invoke(Participant target)
        {
            target.TakeDamage(Value);
        }
    }
}