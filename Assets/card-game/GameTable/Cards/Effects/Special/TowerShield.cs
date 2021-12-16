public class TowerShield : CardEffect
{
    public TowerShield(Card thisCard) : base(thisCard)
    {
    }

    public override void PlaceEffect(int value)
    {
        base.PlaceEffect(value);
        AddArmor(Enemy, value);
    }

    public override void RemoveEffect(int value)
    {
        base.RemoveEffect(value);
        Burn(_thisCard);
    }

    public override void Play(int value, Participant target)
    {
        AddArmor(target, value);
    }
}
