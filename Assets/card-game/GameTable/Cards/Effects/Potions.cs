public class Potions : CardEffect
{
    public Potions(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Burn(_thisCard);
    }

    public override void Play(int value, Participant target)
    {
        Heal(target, value);
    }
}
