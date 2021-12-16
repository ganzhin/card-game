public class Worm : CardEffect
{
    public Worm(Card thisCard) : base(thisCard)
    {
    }

    public override void Play(int value)
    {
        Burn(_thisCard);

    }
}
