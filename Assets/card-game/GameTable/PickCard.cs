using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickCard
{
     public static void SaveInDeck(Card card, bool loadMap = true)
    {
        if (File.Exists($"{Application.dataPath}/Save/deck.xml"))
        {
            var serializer = new XmlSerializer(typeof(DeckData));
            var stream = new FileStream($"{Application.dataPath}/Save/deck.xml", FileMode.Open);

            DeckData loadedDeckData = serializer.Deserialize(stream) as DeckData;
            stream.Close();

            loadedDeckData.CardNames.Add(card.name);

            loadedDeckData.Save();
        }

        if (loadMap) SceneLoader.LoadScene("MapScene");
    }
}