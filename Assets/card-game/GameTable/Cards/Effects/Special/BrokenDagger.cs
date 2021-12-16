public class BrokenDagger : CardEffect
{
    public BrokenDagger(Card thisCard) : base(thisCard)
    {
    }

    public override void Play(int value)
    {
        GetBuffFromOtherCard(Suit.Knives, 3);
    }
    public override void Play(int value, Participant target)
    {
        Attack(target, value);
    }
    public override void AfterPlay()
    {
        Burn(_thisCard);
    }
}
