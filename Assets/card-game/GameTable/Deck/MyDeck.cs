using System.Collections.Generic;
using UnityEngine;

public class MyDeck : MonoBehaviour
{
    private List<Card> _cards = new List<Card>();
    private DeckData _deckData;

    [SerializeField] private Transform _showDeckTransform;

    [SerializeField] private int _columns;
    [SerializeField] private float _width, _height;

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _newPosition;
    private bool _mouseOver;

    private void Start()
    {
        _startPosition = _showDeckTransform.position;
        _newPosition = _startPosition;

    }

    private void OnMouseDrag()
    {
        _newPosition += Vector3.right * Input.GetAxis("Mouse X") * Time.deltaTime * .25f;
    }

    private void OnMouseEnter()
    {
        _mouseOver = true;
    }

    private void OnMouseExit()
    {
        _mouseOver = false;
    }

    private void Update()
    {
        if (_cards.Count != 0)
        {
            if (!_mouseOver && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0)))
            {
                HideDeck();
            }
        }

        _newPosition += Vector3.right * Input.GetAxis("Mouse ScrollWheel") * .25f;
        _newPosition.x = Mathf.Clamp(_newPosition.x, -_cards.Count * _width, _startPosition.x);
        _showDeckTransform.localPosition = Vector3.Lerp(_showDeckTransform.localPosition, _newPosition, Time.deltaTime * 2.25f);
    }

    public void ShowDeck()
    {
        GetComponent<Collider>().enabled = true;

        _showDeckTransform.localPosition = _startPosition + Vector3.right;
        _newPosition = _startPosition;
        _deckData = DeckData.Load();

        int tempValue;
        int tempSuit;
        for (int i = 0; i < _deckData.Suits.Count - 1; i++)
        {
            for (int j = 0; j < _deckData.Suits.Count - i - 1; j++)
            {
                if (_deckData.Suits[j + 1] < _deckData.Suits[j])
                {
                    tempSuit = _deckData.Suits[j + 1];
                    tempValue = _deckData.Values[j + 1];
                    _deckData.Values[j + 1] = _deckData.Values[j];
                    _deckData.Suits[j + 1] = _deckData.Suits[j];

                    _deckData.Suits[j] = tempSuit;
                    _deckData.Values[j] = tempValue;
                }
            }
        }
        /* if (FindObjectOfType<Deck>())
        {

        }
        else */
        {
            int row = 0;
            int column = 0;
            for (int i = 0; i < _deckData.Values.Count; i++)
            {
                int value = _deckData.Values[i];
                int suit = _deckData.Suits[i];

                var card = Instantiate(Resources.Load<Card>(nameof(Card)), _showDeckTransform);
                card.transform.localPosition += column * _width * Vector3.right + row * _height * Vector3.down;
                card.Initialize(value, (Suit)suit, null);

                column++;
                if (column >= _columns)
                {
                    row++;
                    column = 0;
                }
                _cards.Add(card);
            }
        }
        FindObjectOfType<MyDeckClick>().Show(_cards.Count);

    }

    private void HideDeck()
    {
        GetComponent<Collider>().enabled = false;

        foreach (var card in _cards)
        {
            Destroy(card.gameObject);
        }
        _cards.Clear();

    }
}
