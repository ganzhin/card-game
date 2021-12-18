using CardEffects;
using UnityEngine;

public class CardVisual : MonoBehaviour
{
    public bool IsHighlighted { get; private set; }

    [Header("References")]
    [SerializeField] private Renderer _suitRenderer;
    [SerializeField] private Renderer[] _miniSuitRenderer;

    [SerializeField] private TextMesh[] _values;

    [SerializeField] private Texture _suitTexture;
    [SerializeField] private Texture _suitTextureMini;

    [SerializeField] private GameObject _burningSuit;

    private AudioClip _sound;
    private AudioSource _audioSource;

    private int _value;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Refresh();
    }

    private void OnMouseEnter()
    {
        IsHighlighted = true;
        Refresh();
    }
    private void OnMouseExit()
    {
        IsHighlighted = false;
        Refresh();
    }

    internal void Init(int value)
    {
        _value = value;
        _burningSuit.SetActive(GetComponent<Burn>());
        Refresh();
    }

    public void Refresh()
    {
        foreach (var textMesh in _values)
        {
            textMesh.text = _value.ToString();
        }

        _suitRenderer.material.mainTexture = _suitTexture;

        foreach (var renderer in _miniSuitRenderer)
        {
            renderer.material.mainTexture = _suitTextureMini;
        }
    }
}
