using UnityEngine;

public class PokerChip : MonoBehaviour
{
    [SerializeField] private AudioClip[] _sounds;
    private int _sounded = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (_sounded < 2)
        {
            SoundDesign.PlayOneShot(_sounds[Random.Range(0, _sounds.Length)]);
            _sounded++;
        }
    }
}
