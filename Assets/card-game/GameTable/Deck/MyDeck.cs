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

    [SerializeField] private AudioClip audioClip;
    private void Start()
    {
        _startPosition = _showDeckTransform.position;
        _newPosition = _startPosition;

    }

    private void OnMouseDrag()
    {
        _newPosition += Vector3.right * Input.GetAxis("Mouse X") * Time.deltaTime * .4f;
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

        if(_deckData.CardNames.Count > 0)
        {
            int row = 0;
            int column = 0;
            for (int i = 0; i < _deckData.CardNames.Count; i++)
            {
                string cardName = _deckData.CardNames[i];

                var card = Instantiate(CardGenerator.GetCard(cardName), _showDeckTransform);
                card.transform.localPosition += column * _width * Vector3.right + row * _height * Vector3.down;
                card.Initialize();

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

        SoundDesign.PlayOneShot(audioClip);

    }

    private void HideDeck()
    {
        GetComponent<Collider>().enabled = false;

        foreach (var card in _cards)
        {
            Destroy(card.gameObject);
        }
        _cards.Clear();

        SoundDesign.PlayOneShot(audioClip);

    }
}
