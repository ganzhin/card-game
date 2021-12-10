using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckVisual
{
    [SerializeField] private Vector3 _minLocalPosition;
    [SerializeField] private Vector3 _maxLocalPosition;

    [SerializeField] private float _maxCards = 32;

    public void Update(Deck deck, int cardCount)
    {
        deck.transform.localPosition = Vector3.Lerp(_minLocalPosition, _maxLocalPosition, cardCount / _maxCards);
    }
}
