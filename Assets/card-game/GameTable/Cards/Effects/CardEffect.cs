using System.Collections;
using UnityEngine;

public abstract class CardEffect
{
    public virtual void PlaceEffect(int value) 
    {
        Board.ChangeCurrentPrice(value);
    }
    public virtual void RemoveEffect(int value) 
    { 
        Board.ChangeCurrentPrice(-value);
    }

    public virtual void Play(Card thisCard, int value) { }
    public virtual void Play(int value, Card target) { }
    public virtual void Play(int value, Participant target) { }
    public virtual void Burn(Card card)
    {
        card.Burn();
    }
    public virtual void Drop(Card card)
    {
        Object.FindObjectOfType<MonoBehaviour>().StartCoroutine(DropRoutine(card));
    }

    public IEnumerator DropRoutine(Card card)
    {
        var timer = 0f;
        Vector3 cardPosition = card.transform.position;
        
        while (timer < 1)
        {
            timer += Time.deltaTime;
            if (timer <= .5f)
            {
                cardPosition += card.transform.up * (-4 * (timer * timer) + 4 * timer) * 0.001f;
            }
            else
            {
                cardPosition -= card.transform.up * (-4 * (timer * timer) + 4 * timer) * 0.0005f;
            }
            card.transform.position = cardPosition;

            yield return null;

        }

        card.Drop();

    }
}