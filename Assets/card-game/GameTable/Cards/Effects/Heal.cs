using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class Heal : CardEffect
    {
        public override void Invoke(Participant target)
        {
            var participants = Object.FindObjectsOfType<Participant>();
            foreach (var participant in participants)
            {
                if (participant != target)
                {
                    participant.Heal(Value);
                }
            }
        }
    }
}