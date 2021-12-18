using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class Burn : CardEffect
    {
        [SerializeField] private AudioClip _burnSound;
        public override void Invoke(Participant target)
        { 
            Burn(ThisCard);
            SoundDesign.PlayOneShot(_burnSound, transform);
        }
    }
}