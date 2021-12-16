using UnityEngine;

public class CardVisual : MonoBehaviour
{
    public bool IsHighlighted { get; private set; }

    [Header("References")]
    [SerializeField] private Renderer _suitRenderer;
    [SerializeField] private Renderer[] _miniSuitRenderer;

    [SerializeField] private TextMesh[] _values;
    [SerializeField] private Texture[] _suitTextures;

    [SerializeField] private Texture[] _miniSuitTextures;
    [SerializeField] private Texture[] _arcaneSuitTextures;

    private AudioClip _sound;
    private AudioSource _audioSource;

    private int _value;
    private Suit _suit;

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

    internal void Init(int value, Suit suit)
    {
        _value = value;
        _suit = suit;
    }

    public void Refresh()
    {
        foreach (var textMesh in _values)
        {
            textMesh.text = _value.ToString();
        }

        _suitRenderer.material.mainTexture = _suitTextures[(int)_suit];

        foreach (var renderer in _miniSuitRenderer)
        {
            if ((int)_suit < 4)
            {
                renderer.material.mainTexture = _miniSuitTextures[(int)_suit];
            }
            else
            {
                renderer.material.mainTexture = _suitTextures[(int)_suit];
            }
        }
    }
}
