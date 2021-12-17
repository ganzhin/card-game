using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class DeckData
{
    public List<string> CardNames;

    public DeckData()
    { }

    public DeckData(Deck deck)
    {
        CardNames = new List<string>();

        List<Card> cards = deck.GetAllCards();
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            CardNames.Add(card.name);
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

    public static DeckData Load()
    {
        if (File.Exists($"{Application.dataPath}/Save/deck.xml"))
        {
            var serializer = new XmlSerializer(typeof(DeckData));
            var stream = new FileStream($"{Application.dataPath}/Save/deck.xml", FileMode.Open);

            DeckData loadedDeckData = serializer.Deserialize(stream) as DeckData;
            stream.Close();

            return loadedDeckData;
        }
        else
        {
            return null;
        }
    }
}