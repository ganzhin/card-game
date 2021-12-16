public class Bag : CardEffect
{
    public Bag(Card thisCard) : base(thisCard)
    { 
    }

    public override void PlaceEffect(int value)
    {
        Burn(_thisCard);
        var x = DropHand();
        for (int i = 0; i < x; i++)
        {
            TakeCard();
        }
    }
}
