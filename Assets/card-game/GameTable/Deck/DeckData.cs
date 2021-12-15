using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class DeckData
{
    public List<int> Values;
    public List<int> Suits;

    public DeckData()
    { }

    public DeckData(Deck deck)
    {
        Values = new List<int>();
        Suits = new List<int>();

        List<Card> cards = deck.GetAllCards();
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            Values.Add(card.GetValue());
            Suits.Add(card.GetSuit());
        }

        Save();
    }

    public void Save()
    {
        if (!File.Exists($"{Application.dataPath}/Save/deck.xml"))
        {
            Directory.CreateDirectory($"{Application.dataPath}/Save");
            var x = File.Create($"{Application.dataPath}/Save/deck.xml");
            x.Close();
        }
        var serializer = new XmlSerializer(typeof(DeckData));
        var stream = new FileStream($"{Application.dataPath}/Save/deck.xml", FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
}