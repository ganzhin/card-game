using System.Collections;
using UnityEngine;

public abstract class CardEffect
{
    internal Player Player => Object.FindObjectOfType<Player>();
    internal Enemy Enemy => Object.FindObjectOfType<Enemy>();

    private int _buff = 0;
    internal Card _thisCard;
    
    internal CardEffect(Card thisCard)
    {
        _thisCard = thisCard;
    }

    public virtual void PlaceEffect(int value)
    {
        Board.ChangeCurrentPrice(value);
    }
    public virtual void RemoveEffect(int value)
    {
        Board.ChangeCurrentPrice(-value);
    }
    public virtual void Play(int value) { }
    public virtual void Play(int value, Card target) { }
    public virtual void Play(int value, Participant target) { }
    public virtual void AfterPlay() { }

    public IEnumerator DropRoutine(Card card)
    {
        var timer = 0f;
        Vector3 cardPosition = card.transform.position;

        while (timer < Settings.LongCardPause)
        {
            timer += Time.deltaTime;
            if (timer <= Settings.LongCardPause / 2f)
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

    public virtual void Burn(Card card)
    {
        card.Burn();
        if (Player._hand.Cards.Contains(card))
        {
            Player._hand.Cards.Remove(card);
        }
    }
    public virtual void Drop(Card card)
    {
        Object.FindObjectOfType<MonoBehaviour>().StartCoroutine(DropRoutine(card));
    }
    public virtual void TakeCard()
    {
        Player.TakeCardFromDeck(false);
    }
    public virtual Card AddCardInDeck(int value, Suit suit)
    {
        var card = Player.InstantiateCardInDeck(value, suit);
        return card;
    }
    public virtual void ShuffleDeck()
    {
        Player._deck.Shuffle();
    }
    public virtual int DropHand()
    {
        int ret = Player._hand.Cards.Count;
        for (int i = Player._hand.Cards.Count - 1; i >= 0; i--)
        {
            Card card = Player._hand.Cards[i];
            card.Drop();
            if (Player._hand.Cards.Contains(card))
            {
                Player._hand.Cards.Remove(card);
            }
        }
        return ret;
    }
    public virtual void Attack(Participant target, int value)
    {
        target.TakeDamage(value + _buff);
       
        _buff = 0;
    }
    public virtual void Heal(Participant target, int value)
    {
        var participants = Object.FindObjectsOfType<Participant>();
        foreach (var participant in participants)
        {
            if (participant != target)
            {
                participant.Heal(value + _buff);
            }
        }
        _buff = 0;
    }
    public virtual void AddArmor(Participant target, int value)
    {
        var participants = Object.FindObjectsOfType<Participant>();
        foreach (var participant in participants)
        {
            if (participant != target)
            {
                participant.AddArmor(value + _buff);
            }
        }
        _buff = 0;
    }
    public virtual int BurnHand()
    {
        int ret = Player._hand.Cards.Count;
        for (int i = Player._hand.Cards.Count - 1; i >= 0; i--)
        {
            Card card = Player._hand.Cards[i];
            card.Burn();
            if (Player._hand.Cards.Contains(card))
            {
                Player._hand.Cards.Remove(card);
            }
        }
        return ret;
    }
    public virtual void UnburnBurned()
    {
        Player._deck.RestoreBurnedCards();
    }
    public virtual void GetBuffFromOtherCard(Suit otherSuit, int value = 1)
    {
        foreach (var check in Board.board.Cards)
        {
            if (check.GetSuit() == (int)otherSuit)
            {
                _buff += value;
            }
        }
    }
}