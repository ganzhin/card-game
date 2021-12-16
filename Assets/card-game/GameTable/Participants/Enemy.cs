﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Participant
{
    [SerializeField] private List<Turn> _turnsVariants;
    [SerializeField] private Transform _cardsPlace;

    [SerializeField] private float _cardWidth;
    [SerializeField] private Card[] _currentCards;
    [SerializeField] private Transform _cardSpawnPoint;

    [SerializeField] private Text _clickToContinueText;

    internal override void Start()
    {
        base.Start();
        foreach (var turn in _turnsVariants)
        {
            for (int i = 0; i < turn.Cards.Length; i++)
            {
                var cardObject = Instantiate(turn.Cards[i]);
                cardObject.Initialize();
                turn.Cards[i] = cardObject;
                cardObject.transform.position = _cardSpawnPoint.position;
            }
        }
    }

    public virtual void MakeMoves()
    {
        StartCoroutine(PlaceCards(Random.Range(0, _turnsVariants.Count)));
    }

    public IEnumerator PlaceCards(int turnIndex)
    {
        yield return new WaitForSeconds(Settings.EnemyTurnPause);

        var cardPlaces = new Vector3[_turnsVariants[turnIndex].Cards.Length];
        for (int i = 0; i < cardPlaces.Length; i++)
        {
            cardPlaces[i] = _cardsPlace.position;
            cardPlaces[i].x = (-(cardPlaces.Length * _cardWidth) / 2) + i * _cardWidth + _cardWidth / 2f;
        }

        for (int i = 0; i < _turnsVariants[turnIndex].Cards.Length; i++)
        {
            Card card = _turnsVariants[turnIndex].Cards[i];
            card.IsOnBoard = true;

            StartCoroutine(MoveCardToPosition(card, cardPlaces[i]));

            yield return new WaitForSeconds(Settings.CardPause);
        }
        yield return new WaitForSeconds(Settings.EnemyTurnPause);

        _currentCards = _turnsVariants[turnIndex].Cards;

        _clickToContinueText.gameObject.SetActive(true);
        while (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
        {
            yield return null;
        }
        _clickToContinueText.gameObject.SetActive(false);

        foreach (var card in _currentCards)
        {
            card.Play();
            yield return new WaitForSeconds(Settings.CardPause);
        }

        yield return new WaitForSeconds(Settings.EnemyTurnPause);

        foreach (var card in _currentCards)
        {
            StartCoroutine(MoveCardToPosition(card, _cardSpawnPoint.position));
        }

        _currentCards = null;

        yield return new WaitForSeconds(Settings.EnemyTurnPause);

        Board.EndTurn();
        
        
    }

    public IEnumerator MoveCardToPosition(Card card, Vector3 position)
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;

            card.transform.position = Vector3.Lerp(card.transform.position, position, timer);
            card.transform.rotation = Quaternion.Lerp(card.transform.rotation, _cardsPlace.rotation, timer);
            yield return null;
        }
        card.UnBurn();
    }

    public override void Death()
    {
        Board.board.Win();
        ChipMoney.Money += Random.Range(1, 6);
        ChipMoney.Health = FindObjectOfType<Player>()._health;
        ChipMoney.Floor++;

    }
}

[System.Serializable]
public class Turn
{
    public Card[] Cards;
}