public class BagEffect : CardEffect
{
    public override void Play(Card thisCard, int value)
    {
        Burn(thisCard);

    }
}
