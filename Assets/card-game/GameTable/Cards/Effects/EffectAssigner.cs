using System;

public static class EffectAssigner
{
    public static CardEffect GetEffect(Card card, int value, Suit suit)
    {
        CardEffect cardEffect = new Arrow(card);
        card.CurrentPrice = value;

        switch (suit)
        {
            case Suit.Branches:
                cardEffect = new Branches(card);
                card.CurrentPrice = -value;
                break;

            default:
                object[] args = { card };
                cardEffect = (CardEffect)Activator.CreateInstance(Type.GetType(suit.ToString()), args);
                break;

        }

        return cardEffect;
    }
}
