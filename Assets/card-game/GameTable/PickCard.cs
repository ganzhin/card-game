using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickCard : MonoBehaviour
{
    [SerializeField] private Card[] _cards;

    private void Start()
    {
        foreach (var card in _cards)
        {
            card.Initialize(Random.Range(2, 7), (Suit)Random.Range(0, 4), null);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Card card;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<Card>())
                    {
                        card = hit.collider.GetComponent<Card>();
                        SaveInDeck(card);
                    }
                }
            }
        }
    }

    private void SaveInDeck(Card card)
    {
        if (File.Exists($"{Application.dataPath}/Save/deck.xml"))
        {
            var serializer = new XmlSerializer(typeof(DeckData));
            var stream = new FileStream($"{Application.dataPath}/Save/deck.xml", FileMode.Open);

            DeckData loadedDeckData = serializer.Deserialize(stream) as DeckData;
            stream.Close();

            loadedDeckData.Values.Add(card.GetValue());
            loadedDeckData.Suits.Add(card.GetSuit());

            loadedDeckData.Save();
        }

        SceneLoader.LoadScene("MapScene");
    }
}