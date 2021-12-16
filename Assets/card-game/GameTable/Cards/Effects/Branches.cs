public class Branches : CardEffect
{
    public Branches(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Drop(_thisCard);
    }
}