public class Balance : CardEffect
{
    public Balance(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Burn(_thisCard);

    }
}
