using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> Cards;

    [SerializeField] private float _cardDistance = 0.05f;
    [SerializeField] private float _cardThickness = 0.002f;
    [SerializeField] private float _highlightHeight = 0.05f;

    [SerializeField] private float _cardSpeed = 4;
    [SerializeField] private float _handshake = 5;

    private void FixedUpdate()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            float x = (-(Cards.Count * _cardDistance) / 2) + i * _cardDistance + _cardDistance / 2;
            float z = _cardThickness * -i;

            if (Cards[i].GetComponent<CardVisual>().IsHighlighted)
            {
                z -= _highlightHeight;
            }

            Vector3 newPosition = new Vector3(x, 0, z);
            Cards[i].transform.localPosition = Vector3.Lerp(Cards[i].transform.localPosition, newPosition, Time.deltaTime * _cardSpeed);
            foreach (Card card in Cards)
            {
                if (card.IsOnBoard)
                {
                    Cards.Remove(card);
                    break;
                }
            }
        }
        transform.localPosition += Mathf.Sin(Time.time) * Vector3.up * .00001f * _handshake;
    }
    
    public void AddCard(Card card)
    {
        card.IsOnBoard = false;
        Cards.Add(card);
        card.transform.parent = transform;

    }
}
