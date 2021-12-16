using UnityEngine;

public static class CardGenerator
{
    public static void GetCard(out int value, out Suit suit, int maxValue = 9)
    {
        value = 1;
        suit = (Suit)Random.Range(0, System.Enum.GetNames(typeof(Suit)).Length);

        value = suit switch
        {
            //Suit.Branches => throw new NotImplementedException(),
            //Suit.Knives => throw new NotImplementedException(),
            //Suit.Potions => throw new NotImplementedException(),
            //Suit.Shields => throw new NotImplementedException(),
            Suit.Arrow => 1,
            Suit.Bow => 3,
            //Suit.Poison => throw new NotImplementedException(),
            Suit.BrokenDagger => 3,
            Suit.Bag => 6,
            //Suit.Divider => throw new System.NotImplementedException(),
            //Suit.Exchange => throw new System.NotImplementedException(),
            Suit.Fire => 1,
            //Suit.Quiver => throw new System.NotImplementedException(),
            Suit.Storm => 1,
            Suit.TowerShield => Random.Range(1, 3),
            Suit.Void => 4,
            _ => Random.Range(2, maxValue)
        };
    }
}
