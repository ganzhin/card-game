public class Exchange : CardEffect
{
    public Exchange(Card thisCard) : base(thisCard)
    {
    }

    public override void AfterPlay()
    {
        Drop(_thisCard);
    }
    public override void Play(int value, Participant target)
    {
        Attack(target, value);
        AddArmor(Player, 2);
    }
}
