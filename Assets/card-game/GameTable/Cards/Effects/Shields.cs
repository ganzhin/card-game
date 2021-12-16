public class Shields: CardEffect
{
    public Shields(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Drop(_thisCard);
    }

    public override void Play(int value, Participant target)
    {
        AddArmor(target, value);
    }
}
