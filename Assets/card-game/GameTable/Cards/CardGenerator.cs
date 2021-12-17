using System.Collections.Generic;
using UnityEngine;

public static class CardGenerator
{
    private static Card[] _commonCards;
    private static Card[] _uncommonCards;
    private static Card[] _rareCards;

    private static List<Card> _allCards = new List<Card>();

    private static int _chanceCommon = 40;
    private static int _chanceUncommon = 35;
    private static int _chanceRare = 25;

    public static Card GetCard()
    {
        _commonCards = Resources.LoadAll<Card>("CommonCards");
        _uncommonCards = Resources.LoadAll<Card>("UncommonCards");
        _rareCards = Resources.LoadAll<Card>("RareCards");

        _allCards.AddRange(_commonCards);
        _allCards.AddRange(_uncommonCards);
        _allCards.AddRange(_rareCards);

        var random = Random.Range(0, 101);
        if (random <= _chanceCommon)
        {
            return _commonCards[Random.Range(0, _commonCards.Length)];

        }
        else if (random <= _chanceCommon + _chanceUncommon)
        {
            return _uncommonCards[Random.Range(0, _uncommonCards.Length)];

        }
        else if (random <= _chanceCommon + _chanceUncommon + _chanceRare)
        {
            return _rareCards[Random.Range(0, _rareCards.Length)];

        }
        else
        {
            return null;

        }    
    }

    public static Card GetCard(string name)
    {
        _commonCards = Resources.LoadAll<Card>("CommonCards");
        _uncommonCards = Resources.LoadAll<Card>("UncommonCards");
        _rareCards = Resources.LoadAll<Card>("RareCards");

        _allCards.AddRange(_commonCards);
        _allCards.AddRange(_uncommonCards);
        _allCards.AddRange(_rareCards);

        foreach (var card in _allCards)
        {
            if (card.name == name)
            {
                return card;
            }
        }
        return null;
    }

    public static Card GetCardWithPrice(out int price)
    {
        price = Random.Range(5, 20);

        return GetCard();
    }
}
