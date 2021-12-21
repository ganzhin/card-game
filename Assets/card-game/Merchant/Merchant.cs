using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    private List<Card> _cards = new List<Card>();

    [SerializeField] private int _cardCount;

    [SerializeField] private Transform _shopCardsRoot;
    [SerializeField] private float _width, _height;

    [SerializeField] private bool _free;
    [SerializeField] private bool _one;

    [SerializeField] private TextMesh _textMeshPrefab;
    [SerializeField] private AudioClip _buyClip;

    private IEnumerator Start()
    {
        for (int column = 0; column < _cardCount; column++)
        {
            var cardTemplate = CardGenerator.GetCardWithPrice(out int price);

            var card = Instantiate(cardTemplate, _shopCardsRoot);
            var text = Instantiate(_textMeshPrefab, card.transform);
            text.transform.localPosition = new Vector3(-45, -110, 0);
            text.transform.localScale = new Vector3(1000, 1000, 1);

            card.name = cardTemplate.name;

            card.transform.localPosition += (-_cardCount / 2f * _width + _width/2f) * Vector3.right + column * _width * Vector3.right;

            card.Initialize();

            card.gameObject.AddComponent<MerchantSellCard>().Price = _free ? 0 : price;
            card.gameObject.GetComponent<MerchantSellCard>().One = _one;
            card.gameObject.GetComponent<MerchantSellCard>().BuyClip = _buyClip;
            text.text = _free ? "" : price.ToString();
            card.gameObject.AddComponent<Floating>();


            _cards.Add(card);
            yield return new WaitForSeconds(Settings.CardPause);
        }
    }

    public void LoadMap()
    {
        SceneLoader.LoadScene("MapScene");
        ChipMoney.Floor++;
    }
}