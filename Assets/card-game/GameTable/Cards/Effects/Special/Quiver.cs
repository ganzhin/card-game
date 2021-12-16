public class Quiver : CardEffect
{
    public Quiver(Card thisCard) : base(thisCard)
    {
    }

    public override void Play(int value)
    {
        for (int i = 0; i < value; i++)
        {
            var card = AddCardInDeck(1, Suit.Arrow);
            TakeCard();
        }
        // Учесть, что карта берется из колоды только если PlayerTurn. Если что ошибка вероятно в этом.
    }
    public override void AfterPlay()
    {
        Drop(_thisCard);
    }
}
