public class Arrow : CardEffect
{
    public Arrow(Card thisCard) : base(thisCard)
    { 
    }

    public override void Play(int value)
    {
        GetBuffFromOtherCard(Suit.Bow);
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
