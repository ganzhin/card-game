using UnityEngine;

namespace CardEffects
{
    public class AddArmor : CardEffect
    {
        public override void Invoke(Participant target)
        {
            var participants = Object.FindObjectsOfType<Participant>();
            foreach (var participant in participants)
            {
                if (participant != target)
                {
                    participant.AddArmor(Value);
                }
            }
        }
    }
}