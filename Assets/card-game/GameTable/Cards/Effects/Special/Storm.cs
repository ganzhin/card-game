public class Storm : CardEffect
{
    public Storm(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Burn(_thisCard);
    }

    public override void Play(int value, Participant target)
    {
        var x = DropHand();
        for (int i = 0; i < x; i++)
        {
            Attack(target, 1);
        }
    }
}
