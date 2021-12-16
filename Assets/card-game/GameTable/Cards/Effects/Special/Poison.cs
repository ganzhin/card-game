public class Poison : CardEffect
{
    public Poison(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Drop(_thisCard);
    }
}
