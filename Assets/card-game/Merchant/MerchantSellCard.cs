using System.Collections;
using UnityEngine;

public class MerchantSellCard : MonoBehaviour
{
    public bool One;
    
    private int _price;
    private bool _sold;
    [SerializeField] private TextMesh _priceText;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            if (_priceText)
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
            StartCoroutine(SellRoutine());

            if (FindObjectOfType<ChipMoney>())
            {
                FindObjectOfType<ChipMoney>().UpdateMoney();
            }

            PickCard.SaveInDeck(GetComponent<Card>(), One);
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
