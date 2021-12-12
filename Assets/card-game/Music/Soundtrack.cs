using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    [SerializeField] private AudioClip _sceneMusic;
    [SerializeField] private AudioClip _sceneAmbient;

    private void Start()
    {
        SoundDesign.SetMusic(_sceneMusic);
        SoundDesign.SetAmbient(_sceneAmbient);
    }
}
