﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Player : Participant
{
    [SerializeField] private Card _cardTemplate;
    private int _maxCardCount = 10;

    [SerializeField] private bool _game;

    internal override void Awake()
    {
        base.Awake();
        if (_game)
        {
            LoadDeck();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Board.PlayerTurn)
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
        /* TEMP */
        for (int j = 2; j < 4; j++)
        {
            InstantiateCardInDeck(j, Suit.branches);
            for (int i = 2; i <= 4; i++)
            {
                InstantiateCardInDeck(i, Suit.knives);

                /* TEMP */
                if (j == 0)
                {
                    InstantiateCardInDeck(i, Suit.potions);
                }

                InstantiateCardInDeck(i, Suit.shields);
                //InstantiateCardInDeck((int)Random.Range(11, 14), (Suit)Random.Range(0, 4));
            }
        }
        _deck.Shuffle();
        for (int i = 0; i < 6; i++)
        {
            TakeCardFromDeck();
            _takenCardsInThisTurn = 0;
        }
    }
    public void AddInHand(Card card)
    {
        _hand.AddCard(card);
    }
    public void RemoveFromHand(Card card)
    {
        if (_hand.Cards.Contains(card) == false) throw new System.ArgumentException();

        _hand.Cards.Remove(card);
    }
    public void InstantiateCardInDeck(int value, Suit suit)
    {
        var card = Instantiate(_cardTemplate);
        card.Initialize(value, suit, _deck);
        card.name = $"{value} of {suit}";
        card.transform.parent = _deck.transform;
        _deck.AddCard(card);
    }
    public void TakeCardFromDeck()
    {
        if (!Board.PlayerTurn) return;

        if (_hand.Cards.Count < _maxCardCount)
        {
            if (_takenCardsInThisTurn == 3) return;

            if (_takenCardsInThisTurn == 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (_deck.TakeCard(true) != null)
                    {
                        _takenCardsInThisTurn++;
                    }
                }
                StartCoroutine(TakeCardsByTime(_takenCardsInThisTurn - 1));

                EndTurn();
            }
            else
            {
                _takenCardsInThisTurn++;
                StartCoroutine(TakeCardsByTime(1));
            }
        }
    }
    public void TakeCardFromShop(Card card, CardShop cardShop = null, int cardIndex = 0)
    {
        if (!Board.PlayerTurn) return;

        if (_hand.Cards.Count < _maxCardCount)
        {
            _takenCardsInThisTurn++;
            _hand.AddCard(card);
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
                _hand.AddCard(_deck.TakeCard());
            }
            yield return new WaitForSeconds(Settings.CardPause);
        }
    }
    public void LoadDeck()
    {
        //TEMP
        //{
        StarterDeck();
        //}
    }
}