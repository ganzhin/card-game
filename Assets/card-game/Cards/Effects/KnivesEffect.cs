public class KnivesEffect : CardEffect
{
    public override void Play(Card thisCard, int value)
    {
        Drop(thisCard);
    }

    public override void Play(int value, Participant target)
    {
        target.TakeDamage(value);
    }
}