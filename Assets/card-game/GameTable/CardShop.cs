using System.Collections;
using System.Linq;
using UnityEngine;

public class CardShop : MonoBehaviour
{
    public Card[] Cards = new Card[5];
    [SerializeField] private Transform[] _places = new Transform[5];
    [SerializeField] private float _cardSpeed = 3;

    private void Start()
    {
        PlaceCards();
    }

    private void Update()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i] != null)
            {
                Cards[i].transform.position = Vector3.Lerp(Cards[i].transform.position, _places[i].transform.position, Time.fixedDeltaTime * _cardSpeed);
                Cards[i].transform.rotation = Quaternion.Lerp(Cards[i].transform.rotation, _places[i].transform.rotation, Time.fixedDeltaTime * _cardSpeed);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            TakeCard();
        }

    }

    private void TakeCard()
    {
        if (Board.PlayerTurn == false) return;

        Card card;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Card>())
                {
                    if (Cards.Contains(hit.collider.GetComponent<Card>()))
                    {
                        card = hit.collider.GetComponent<Card>();
                        for (int i = 0; i < Cards.Length; i++)
                        {
                            if (Cards[i] == card)
                            {                      
                                FindObjectOfType<Player>().TakeCardFromShop(card, this, i);
                                
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void PlaceCards()
    {
        StartCoroutine(PlaceCardsByTime());
    }

    private IEnumerator PlaceCardsByTime()
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i] == null)
            {
                if (FindObjectOfType<Deck>().TakeCard(true) != null)
                {
                    Cards[i] = FindObjectOfType<Deck>().TakeCard();
                    Cards[i].IsOnBoard = true;
                    yield return new WaitForSeconds(Settings.CardPause);
                }
            }
        }
    }
}
