public class Fire : CardEffect
{
    public Fire(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Burn(_thisCard);
    }

    public override void Play(int value, Participant target)
    {
        var x = BurnHand();
        for (int i = 0; i < x; i++)
        {
            Attack(target, 1);
        }
    }
}
