public class Void : CardEffect
{
    public Void(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Burn(_thisCard);

    }

    public override void Play(int value, Participant target)
    {
        Attack(target, value);
        ShuffleDeck();
    }
}
