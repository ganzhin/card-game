using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CardVisual))]
public class Card : MonoBehaviour
{
    public bool IsOnBoard;
    public bool IsBurned;
    public bool IsDropped;

    public Deck OwnerDeck { get; private set; }

    [Header("Card Info")]
    [SerializeField] private int _value;

    [SerializeField] private CardEffects.CardEffect[] _onPlace;
    [SerializeField] private CardEffects.CardEffect[] _onRemove;
    [SerializeField] private CardEffects.CardEffect[] _onPlay;
    [SerializeField] private CardEffects.CardEffect[] _onAfterPlay;

    private CardVisual _cardVisual;
    [SerializeField] private Renderer _faceRenderer;
    [SerializeField] private Material _burnMaterial;
    [SerializeField] private Material _transparentMaterial;
    private List<Material> _cardMaterials = new List<Material>();

    [SerializeField] private AudioClip _cardFluff;
    [SerializeField] private AudioClip _cardDeal;

    private void OnMouseDown()
    {
        if (!Board.board) return;
        if (!Board.board.PlayerTurn) return;
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

    public void Initialize(Deck ownerDeck)
    {
        OwnerDeck = ownerDeck;
        Initialize();
    }

    public void Initialize()
    {
        _cardVisual = GetComponent<CardVisual>();
        _cardVisual.Init(_value);
        _cardVisual.Refresh();

        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            _cardMaterials.Add(renderer.material);
        }
    }

    internal void AfterPlay()
    {
        foreach (var effect in _onAfterPlay)
        {
            effect.Invoke(FindObjectOfType<Player>());
        }
    }

    public void Play()
    {
        foreach (var effect in _onPlay)
        {
            if (Board.board.PlayerTurn)
            {
                effect.Invoke(FindObjectOfType<Enemy>());
            }
            else
            {
                effect.Invoke(FindObjectOfType<Player>());
            }
        }
    }

    public void PlaceEffect()
    {
        foreach (var effect in _onPlace)
        {
            effect.Invoke(FindObjectOfType<Player>());
        }
        SoundDesign.PlayOneShot(_cardDeal, transform);
    }

    public void RemoveEffect()
    {
        foreach (var effect in _onRemove)
        {
            effect.Invoke(FindObjectOfType<Player>());
        }
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

        if (Board.board.Cards.Contains(this))
        {
            Board.board.Cards.Remove(this);
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
            if (timer > .8f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + Vector3.down * .01f, Time.deltaTime);
            }
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                if (renderer.material.HasColor("_Color"))
                {
                    renderer.material.color = Color.Lerp(renderer.material.color, Color.clear, timer / 1.2f);
                }
            }

            yield return null;
        }
        transform.position = Vector3.down;
        if (Board.board.Cards.Contains(this))
        {
            Board.board.Cards.Remove(this);
        }
    }

    public void UnBurn()
    {
        IsBurned = false;
        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
        {
            GetComponentsInChildren<Renderer>()[i].material = _cardMaterials[i];
            if (GetComponentsInChildren<Renderer>()[i].material.HasColor("_Color"))
            {
                GetComponentsInChildren<Renderer>()[i].material.color = Color.white;
            }
        }
    }

    public void Drop()
    {
        if (OwnerDeck != null)
        {
            OwnerDeck.DropCard(this);
        }
        IsDropped = true;
        if (Board.board.Cards.Contains(this))
        {
            Board.board.Cards.Remove(this);
        }
    }

    public int GetValue()
    {
        return _value;
    }

}