using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardVisual))]
public class Card : MonoBehaviour
{
    public bool IsOnBoard;
    public bool IsBurned;
    public bool IsDropped;

    public Deck OwnerDeck { get; private set; }

    public int CurrentPrice;
    public int CurrentEffect;

    [Header("Card Info")]
    [SerializeField] private int _value;
    [SerializeField] private Suit _suit;

    private CardEffect _cardEffect;

    private CardVisual _cardVisual;
    [SerializeField] private Renderer _faceRenderer;
    [SerializeField] private Material _burnMaterial;
    [SerializeField] private Material _transparentMaterial;
    private List<Material> _cardMaterials = new List<Material>();

    [SerializeField] private AudioClip _cardFluff;
    [SerializeField] private AudioClip _cardPlay;

    private void OnMouseDown()
    {
        if (!Board.PlayerTurn) return;
        if (IsOnBoard)
        {
            if (FindObjectOfType<Hand>().Cards.Count < 10)
                RemoveFromBoard();
        }
        else
        {
            AddOnBoard();
        }
    }

    private void OnMouseEnter()
    {
        SoundDesign.SoundOneShot(_cardFluff, transform);
    }

    public void Initialize(int value, Suit suit, Deck ownerDeck)
    {
        _value = value;
        _suit = suit;
        OwnerDeck = ownerDeck;

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            _cardMaterials.Add(renderer.material);
        }

        Initialize();
    }

    public void Initialize()
    {
        _cardVisual = GetComponent<CardVisual>();
        _cardVisual.Init(_value, _suit);
        _cardVisual.Refresh();
        _cardEffect = EffectAssigner.GetEffect(this, _value, _suit);

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            _cardMaterials.Add(renderer.material);
        }
    }

    public void Play()
    {
        _cardEffect.Play(this, _value);
        if (Board.PlayerTurn)
        {
            _cardEffect.Play(_value, FindObjectOfType<Enemy>());
        }
        else
        {
            _cardEffect.Play(_value, FindObjectOfType<Player>());
        }
    }

    public void PlaceEffect()
    {
        _cardEffect.PlaceEffect(CurrentPrice);
    }

    public void RemoveEffect()
    {
        _cardEffect.RemoveEffect(CurrentPrice);
    }

    public void AddOnBoard()
    {
        Board.board.PlaceCard(this);
    }

    public void RemoveFromBoard()
    {
        IsOnBoard = false;
        Board.board.RemoveCard(this);
    }

    public void Burn()
    {
        if (OwnerDeck != null)
        {
            OwnerDeck.BurnCard(this);

            IsBurned = true;
        }

        _faceRenderer.material = _burnMaterial;
        StartCoroutine(BurnByTime());
    }

    private IEnumerator BurnByTime()
    {
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            _faceRenderer.material.SetFloat("_value", timer);
            if (timer > 1f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + Vector3.down * .01f, Time.deltaTime);
            }
            yield return null;
        }
        transform.position = Vector3.down;
    }

    public void UnBurn()
    {
        IsBurned = false;
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
        {
            GetComponentsInChildren<Renderer>()[i].material = _cardMaterials[i];
        }
    }

    public void Drop()
    {
        if (OwnerDeck != null)
        {
            OwnerDeck.DropCard(this);
        }
        IsDropped = true;
    }
 
    public void Save()
    { 
    
    }

}