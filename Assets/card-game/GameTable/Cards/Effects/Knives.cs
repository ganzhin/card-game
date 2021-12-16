public class Knives : CardEffect
{
    public Knives(Card thisCard) : base(thisCard)
    {
    }

    public override void Play(int value)
    {
        GetBuffFromOtherCard(Suit.Poison);
        GetBuffFromOtherCard(Suit.BrokenDagger, -1000);
    }

    public override void Play(int value, Participant target)
    {
        Attack(target, value);
    }
    public override void AfterPlay()
    {
        Drop(_thisCard);
    }
}