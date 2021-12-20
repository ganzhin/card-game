using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Player : Participant
{
    [SerializeField] private Card _cardTemplate;
    private int _maxCardCount = 10;

    [SerializeField] private bool _game;

    internal override void Start()
    {
        if (FindObjectOfType<DialogueTutorial>() == null)
        {
            _health = ChipMoney.Health;
            _maxHealth = ChipMoney.MaxHealth;
        }

        base.Start();
        if (_game)
        {
            LoadDeck();
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Board.board.PlayerTurn)
        {
            PlayCardsOnBoard();
        }
    }
    public void PlayCardsOnBoard()
    {
        Board.board.PlayCards();
    }
    private void StarterDeck()
    {
        InstantiateCardInDeck("2 of potions");
        InstantiateCardInDeck("3 of potions");
        
        for (int i = 2; i <= 4; i++)
        {
            InstantiateCardInDeck($"{i} of knives");
            InstantiateCardInDeck($"{i} of knives");
            InstantiateCardInDeck($"{i} of shields");
            InstantiateCardInDeck($"{i} of branches");
        }
        SaveDeck();

        _deck.Shuffle();

        for (int i = 0; i < 6; i++)
        {
            TakeCardFromDeck(false);
        }
    }
    public void AddInHand(Card card)
    {
        _hand.AddCard(card);
        card.GetComponent<Collider>().enabled = true;

    }
    public void RemoveFromHand(Card card)
    {
        if (_hand.Cards.Contains(card) == false) throw new System.ArgumentException();

        _hand.Cards.Remove(card);
    }
    public Card InstantiateCardInDeck(string name)
    {
        var card = Instantiate(CardGenerator.GetCard(name));
        card.name = name;
        card.transform.parent = _deck.transform;
        card.Initialize(_deck);
        _deck.AddCard(card);

        return card;
    }

    public Card InstantiateCardInDeck(Card card) 
    {
        var newCard = Instantiate(card);
        newCard.Initialize();
        newCard.name = card.name;
        newCard.transform.parent = _deck.transform;
        _deck.AddCard(newCard);

        return newCard;
    }

    public void TakeCardFromDeck(bool cost = true)
    {
        if (!Board.board.PlayerTurn && cost) return;

        if (_hand.Cards.Count < _maxCardCount)
        {
            if (_takenCardsInThisTurn == 3 && cost) return;

            if (_takenCardsInThisTurn == 1 && cost)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (_deck.TakeCard(true) != null)
                    {
                        if (cost)
                        {
                            _takenCardsInThisTurn++;
                        }
                    }
                }
                StartCoroutine(TakeCardsByTime(_takenCardsInThisTurn - 1));

                EndTurn();
            }
            else
            {
                if (cost)
                {
                    _takenCardsInThisTurn++;
                }
                StartCoroutine(TakeCardsByTime(1));
            }
        }
    }
    public void TakeCardFromShop(Card card, CardShop cardShop = null, int cardIndex = 0)
    {
        if (!Board.board.PlayerTurn) return;

        if (_hand.Cards.Count < _maxCardCount)
        {
            _takenCardsInThisTurn++;
            AddInHand(card);
            if (cardShop)
            {
                cardShop.Cards[cardIndex].IsOnBoard = false;
                cardShop.Cards[cardIndex] = null;
            }
            if (_takenCardsInThisTurn == 2)
            {
                EndTurn();
            }
        }
    }
    public IEnumerator TakeCardsByTime(int cardCount)
    {
        for (int i = 0; i < cardCount; i++)
        {
            if (_deck.TakeCard(true) != null)
            {
                AddInHand(_deck.TakeCard());
            }
            yield return new WaitForSeconds(Settings.CardPause);
        }
    }
    public void LoadDeck()
    {
        if (DeckData.Load() != null)
        {
            DeckData loadedDeckData = DeckData.Load();

            for (int i = 0; i < loadedDeckData.CardNames.Count; i++)
            {
                InstantiateCardInDeck(loadedDeckData.CardNames[i]);
            }

            _deck.Shuffle();

            for (int i = 0; i < 6; i++)
            {
                TakeCardFromDeck(false);
            }
        }
        else
        {
            StarterDeck();
        }
    }
    public void SaveDeck()
    {
        var data = new DeckData(_deck);
    }
    public override void Death()
    {
        Board.board.Lose();
    }
}