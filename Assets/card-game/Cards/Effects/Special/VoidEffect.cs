public class VoidEffect : CardEffect
{
    public override void Play(Card thisCard, int value)
    {
        Burn(thisCard);

    }
}
