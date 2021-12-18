using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeckVisual))]
public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> _cards = new List<Card>();
    [SerializeField] private List<Card> _burned = new List<Card>();
    [SerializeField] private List<Card> _draw = new List<Card>();

    [SerializeField] private Transform _drawDeckTransform;

    private bool _isHighlighted;

    [SerializeField] private Transform _topCard;
    [SerializeField] private Transform _middleCards;
    private Vector3 _topCardPosition;
    [SerializeField] private float _cardLift = 0.03f;
    [SerializeField] private float _cardLiftYAngle = -30f;

    [SerializeField] private bool _playersDeck = false;

    [SerializeField] private DeckVisual _deckVisual = new DeckVisual();
    [SerializeField] private AudioClip _shuffleSound;

    private void Start()
    {
        _topCardPosition = _topCard.localPosition;
    }

    private void OnMouseEnter()
    {
        if (_playersDeck)
            _isHighlighted = true;
    }

    private void OnMouseExit()
    {
        if (_playersDeck)
            _isHighlighted = false;
    }

    private void OnMouseDown()
    {
        FindObjectOfType<Player>().TakeCardFromDeck();
    }

    private void Update()
    {
        _deckVisual.Update(this, _cards.Count);

        Vector3 lift = Vector3.up * (_isHighlighted ? _cardLift : 0);
        var angle = Quaternion.Euler(90, _cardLiftYAngle * (_isHighlighted ? 1 : 0), 0);

        _topCard.localPosition = Vector3.Lerp(_topCard.localPosition, _topCardPosition + lift, Time.fixedDeltaTime * 4);
        _topCard.localRotation = Quaternion.Lerp(_topCard.localRotation, angle, Time.fixedDeltaTime * 4);

        foreach (var card in _draw)
        {
            card.transform.position = Vector3.Lerp(card.transform.position, _drawDeckTransform.position, Time.fixedDeltaTime);
            card.transform.rotation = Quaternion.Lerp(card.transform.rotation, _drawDeckTransform.rotation, Time.fixedDeltaTime * 4);
        }

        foreach (var card in _cards)
        {
            card.transform.position = Vector3.Lerp(card.transform.position, _middleCards.position, Time.fixedDeltaTime);
            card.transform.localRotation = Quaternion.Lerp(card.transform.localRotation, Quaternion.Euler(-90, 0, 0), Time.fixedDeltaTime * 4);

        }

        if (_cards.Count == 0) Shuffle();
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void BurnCard(Card card)
    {
        if (_cards.Contains(card))
            _cards.Remove(card);

        _burned.Add(card);
    }
    public void DropCard(Card card)
    {
        if (_cards.Contains(card))
            _cards.Remove(card);

        _draw.Add(card);
        
    }

    public void RestoreBurnedCards()
    {
        foreach (var card in _burned)
        {
            card.UnBurn();
        }
        _cards.AddRange(_burned);
    }

    public void Shuffle()
    {
        if (_draw.Count > 0)
            SoundDesign.PlayOneShot(_shuffleSound, transform);

        _cards.AddRange(_draw);
        _draw.Clear();

        foreach (var card in _cards)
        {
            card.IsBurned = false;
            card.IsOnBoard = false;
            card.IsDropped = false;

            card.transform.parent = transform;
        }

        for (int n = _cards.Count - 1; n > 0; --n)
        {
            int k = Random.Range(0, n + 1);
            Card temp = _cards[n];
            _cards[n] = _cards[k];
            _cards[k] = temp;
        }
    }

    public Card TakeCard(bool check = false)
    {
        if (_cards.Count > 0)
        {
            Card card = _cards[_cards.Count - 1];

            if (!check)
            {
                card.transform.position = _topCard.transform.position;
                card.transform.rotation = _topCard.transform.rotation;

                _cards.Remove(card);
            }

            return card;
        }
        else
        {
            Shuffle();
            return null;
        }
    }

    public List<Card> GetAllCards()
    {
        List<Card> allCards = new List<Card>();

        allCards.AddRange(_cards);
        allCards.AddRange(_burned);
        allCards.AddRange(_draw);
        allCards.AddRange(FindObjectOfType<Hand>().Cards);

        return allCards;
    }
}
