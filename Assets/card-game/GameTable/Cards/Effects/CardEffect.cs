using System.Collections;
using UnityEngine;

namespace CardEffects
{
    [System.Serializable]
    public abstract class CardEffect : MonoBehaviour
    {
        public int Value;
        internal int _buff;

        internal Player Player => FindObjectOfType<Player>();
        internal Enemy Enemy => FindObjectOfType<Enemy>();
        internal Card ThisCard => GetComponent<Card>();

        public abstract void Invoke(Participant target);
        public virtual void PlaceEffect(int value)
        {
            Board.ChangeCurrentPrice(value);
        }
        public virtual void RemoveEffect(int value)
        {
            Board.ChangeCurrentPrice(-value);
        }
        public virtual void AfterPlay() { }
        
        public IEnumerator DropRoutine(Card card)
        {
            var timer = 0f;
            Vector3 cardPosition = card.transform.position;

            while (timer < Settings.LongCardPause)
            {
                timer += Time.deltaTime;
                if (timer <= Settings.LongCardPause / 2f)
                {
                    cardPosition += card.transform.up * (-4 * (timer * timer) + 4 * timer) * 0.001f;
                }
                else
                {
                    cardPosition -= card.transform.up * (-4 * (timer * timer) + 4 * timer) * 0.0005f;
                }
                card.transform.position = cardPosition;

                yield return null;

            }

            card.Drop();

        }

        public virtual void Burn(Card card)
        {
            card.Burn();
            if (Player._hand.Cards.Contains(card))
            {
                Player._hand.Cards.Remove(card);
            }
        }
        public virtual void Drop(Card card)
        {
            StartCoroutine(DropRoutine(card));
        }

        public virtual void ShuffleDeck()
        {
            Player._deck.Shuffle();
        }
        public virtual int DropHand()
        {
            int ret = Player._hand.Cards.Count;
            for (int i = Player._hand.Cards.Count - 1; i >= 0; i--)
            {
                Card card = Player._hand.Cards[i];
                card.Drop();
                if (Player._hand.Cards.Contains(card))
                {
                    Player._hand.Cards.Remove(card);
                }
            }
            return ret;
        }
        public virtual void Attack(Participant target, int value)
        {
            target.TakeDamage(value + _buff);

            _buff = 0;
        }
        public virtual void Heal(Participant target, int value)
        {
            var participants = Object.FindObjectsOfType<Participant>();
            foreach (var participant in participants)
            {
                if (participant != target)
                {
                    participant.Heal(value + _buff);
                }
            }
            _buff = 0;
        }
        public virtual void AddArmor(Participant target, int value)
        {
            var participants = Object.FindObjectsOfType<Participant>();
            foreach (var participant in participants)
            {
                if (participant != target)
                {
                    participant.AddArmor(value + _buff);
                }
            }
            _buff = 0;
        }
        public virtual int BurnHand()
        {
            int ret = Player._hand.Cards.Count;
            for (int i = Player._hand.Cards.Count - 1; i >= 0; i--)
            {
                Card card = Player._hand.Cards[i];
                card.Burn();
                if (Player._hand.Cards.Contains(card))
                {
                    Player._hand.Cards.Remove(card);
                }
            }
            return ret;
        }
        public virtual void UnburnBurned()
        {
            Player._deck.RestoreBurnedCards();
        }
    }
}