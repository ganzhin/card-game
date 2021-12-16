public class Bow : CardEffect
{
    public Bow(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Drop(_thisCard);
    }
}
