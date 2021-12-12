using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickCard : MonoBehaviour
{
    private Card[] _cards;

    private void Start()
    {
        foreach (var card in _cards)
        {
            card.Initialize(Random.Range(2, 6), (Suit)Random.Range(0, 4), null);
        }
    }

    private void Update()
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

    private void SaveInDeck(Card card)
    {
        card.Save();
        SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}