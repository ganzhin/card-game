using TMPro;
using UnityEngine;

public abstract class Participant : MonoBehaviour
{
    [SerializeField] internal Hand _hand;
    [SerializeField] internal Deck _deck;
    internal Board _board;

    [SerializeField] internal int _health = 20;
    [SerializeField] internal bool _isDead;
    [SerializeField] internal Bar _healthBar;
    [SerializeField] internal Bar _armorBar;
    [SerializeField] internal int _armor;

    internal int _takenCardsInThisTurn = 0;

    public virtual void ChangeHealth(int value)
    {
        if (value < 0)
        {
            if (_armor > 0)
            {
                _armor += value;
                if (_armor < 0)
                {
                    _health += _armor;
                    _armor = 0;
                }
            }
            else
            {
                _health += value;
            }
        }

        _health = Mathf.Clamp(_health, 0, 20);

        if (_health < 1) _isDead = true;

        _healthBar.SetValue(_health);
        _armorBar.SetValue(_armor);
    }
    public virtual void AddArmor(int value) 
    {
        _armor += value;
        _armorBar.SetValue(_armor);
    }
    public virtual void EndTurn()
    {
        _takenCardsInThisTurn = 0;
        Board.EndTurn();
    }
}
