using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> Cards;

    [SerializeField] private float _cardDistance = 0.05f;
    [SerializeField] private float _cardThickness = 0.002f;
    [SerializeField] private float _highlightHeight = 0.05f;
    [SerializeField] private float _handRadius = 0.05f;

    [SerializeField] private float _cardSpeed = 4;
    [SerializeField] private float _handshake = 5;


    private void FixedUpdate()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            float x = (-(Cards.Count * _cardDistance) / 2) + i * _cardDistance + _cardDistance / 2;
            float z = _cardThickness * -i;
            float y = -Mathf.Abs(x)/2f * _handRadius;

            if (Cards[i].GetComponent<CardVisual>().IsHighlighted)
            {
                z -= _highlightHeight;
            }

            Vector3 newPosition = new Vector3(x, y, z);
            Cards[i].transform.localPosition = Vector3.Lerp(Cards[i].transform.localPosition, newPosition, Time.deltaTime * _cardSpeed);
            foreach (Card card in Cards)
            {
                if (card.IsOnBoard)
                {
                    Cards.Remove(card);
                    break;
                }
            }

            var zRotation = Mathf.Lerp(8, -8, (float)i / (float)Cards.Count);
            Cards[i].transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, zRotation), Quaternion.Euler(-40, 0, zRotation), Cards[i].transform.localPosition.y / 0.06f);
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
