public class Divider : CardEffect
{
    public Divider(Card thisCard) : base(thisCard)
    {
    }

    public override void Play(int value)
    {
        Attack(Player, value);
        Attack(Enemy, value);
    }
    public override void AfterPlay()
    {
        Drop(_thisCard);
    }
}
