using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public class Drop : CardEffect
    {
        [SerializeField] private AudioClip _audioClip;

        public override void Invoke(Participant target)
        { 
            Drop(ThisCard);
            Camera.main.Shake(.005f);
            SoundDesign.PlayOneShot(_audioClip, transform);
        }
    }
}