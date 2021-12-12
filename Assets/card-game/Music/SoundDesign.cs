using UnityEngine;

public class SoundDesign : MonoBehaviour
{
    static SoundDesign singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _ambientSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _stereoSoundSource;

    public static void SetMusic(AudioClip clip)
    {
        if (singleton._musicSource.clip != clip)
        {
            if (clip == null)
            {
                singleton._musicSource.loop = false;
            }
            else
            {
                singleton._musicSource.loop = true;

                singleton._musicSource.clip = clip;
                singleton._musicSource.Play();
            }
        }
    }

    public static void SetAmbient(AudioClip clip)
    {
        if (singleton._ambientSource.clip != clip)
        {
            singleton._ambientSource.clip = clip;
            singleton._ambientSource.Play();
        }
    }

    public static void SoundOneShot(AudioClip clip, Transform soundTransform = null)
    {
        if (soundTransform)
        {
            singleton._stereoSoundSource.transform.position = soundTransform.position;
            singleton._stereoSoundSource.PlayOneShot(clip);
        }
        else
        {
            singleton._soundSource.PlayOneShot(clip);
        }
    }
}
