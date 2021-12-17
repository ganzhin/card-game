using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    private List<Card> _cards = new List<Card>();

    [SerializeField] private int _cardCount;

    [SerializeField] private Transform _shopCardsRoot;
    [SerializeField] private float _width, _height;

    private IEnumerator Start()
    {
        for (int column = 0; column < _cardCount; column++)
        {
            var card = Instantiate(Resources.Load<Card>("Card with Price"), _shopCardsRoot);

            card.transform.localPosition += (-_cardCount / 2f * _width + _width/2f) * Vector3.right + column * _width * Vector3.right;

            int price = CardGenerator.GetCardWithPrice(out int value, out Suit suit);

            card.Initialize(value, suit, null);
            card.GetComponent<MerchantSellCard>().Price = price;

            _cards.Add(card);
            yield return new WaitForSeconds(Settings.CardPause);
        }
    }

    public void LoadMap()
    {
        SceneLoader.LoadScene("MapScene");
    }
}