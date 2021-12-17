using System.Collections;
using UnityEngine;

public class MerchantSellCard : MonoBehaviour
{
    private int _price;
    private bool _sold = false;
    [SerializeField] private TextMesh _priceText;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            _priceText.text = value.ToString();
        }
    }

    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
    private void OnMouseDown()
    {
        if (_sold) return;
        if (ChipMoney.Money >= Price)
        {
            ChipMoney.Money -= Price;
            _sold = true;
            PickCard.SaveInDeck(GetComponent<Card>(), false);
            StartCoroutine(SellRoutine());
            FindObjectOfType<ChipMoney>().UpdateMoney();
        }
    }
    private IEnumerator SellRoutine()
    {
        var timer = 0f;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, Vector3.up, timer / 1f);
            
            yield return null;
        }

        
    }


}
