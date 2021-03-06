using System.Collections;
using System.Linq;
using UnityEngine;

public class CardShop : MonoBehaviour
{
    public Card[] Cards = new Card[5];
    [SerializeField] private Transform[] _places = new Transform[5];
    [SerializeField] private float _cardSpeed = 3;

    [SerializeField] private AudioClip _cardDeal;
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
                Cards[i].transform.position = Vector3.Lerp(Cards[i].transform.position, _places[i].transform.position, Time.deltaTime * _cardSpeed);
                Cards[i].transform.rotation = Quaternion.Lerp(Cards[i].transform.rotation, _places[i].transform.rotation, Time.deltaTime * _cardSpeed);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            TakeCard();
        }

    }

    private void TakeCard()
    {
        if (Board.board.PlayerTurn == false) return;

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
                                SoundDesign.PlayOneShot(_cardDeal, transform);

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
                    Cards[i].GetComponent<Collider>().enabled = true;

                    SoundDesign.PlayOneShot(_cardDeal, transform);
                    yield return new WaitForSeconds(Settings.CardPause);
                }
            }
        }
    }
}
