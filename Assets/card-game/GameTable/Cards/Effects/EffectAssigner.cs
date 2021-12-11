public static class EffectAssigner
{
    public static CardEffect GetEffect(Card card, int value, Suit suit)
    {
        CardEffect cardEffect = new BalanceEffect();
        card.CurrentPrice = value;
        switch (value)
        {
            case 11:
                cardEffect = new BalanceEffect();
                card.CurrentPrice = 5;
                break;
            case 12:
                cardEffect = new WormEffect();
                card.CurrentPrice = 10;
                break;
            case 13:
                cardEffect = new BagEffect();
                card.CurrentPrice = 0;
                break;
            case 14:
                cardEffect = new VoidEffect();
                card.CurrentPrice = 0;
                break;
            case 15:
                cardEffect = new ArrowEffect();
                card.CurrentPrice = 5;
                break;
            default:
                switch (suit)
                {
                    case Suit.branches:
                        cardEffect = new BranchesEffect();
                        card.CurrentPrice = -value;
                        break;
                    case Suit.knives:
                        cardEffect = new KnivesEffect();
                        break;
                    case Suit.potions:
                        cardEffect = new PotionsEffect();
                        break;
                    case Suit.shields:
                        cardEffect = new ShieldsEffect();
                        break;
                }
                break;
        }
        return cardEffect;
    }
}
