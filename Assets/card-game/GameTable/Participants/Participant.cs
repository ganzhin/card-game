using TMPro;
using UnityEngine;

public abstract class Participant : MonoBehaviour
{
    [SerializeField] internal Hand _hand;
    [SerializeField] internal Deck _deck;
    internal Board _board;

    [SerializeField] internal int _health;
    [SerializeField] internal int _maxHealth = 12;
    [SerializeField] internal Bar _healthBar;
    [SerializeField] internal Bar _armorBar;
    [SerializeField] internal int _armor;

    [SerializeField] internal bool _keepArmor = false; 

    internal int _takenCardsInThisTurn = 0;

    internal virtual void Start()
    {
        TakeDamage(0);
        AddArmor(0);
    }

    public virtual void TakeDamage(int value)
    {
        _healthBar.SetValue(_health);
        _armorBar.SetValue(_armor);

        if (value <= 0) return;

        if (_armor > 0)
        {
            _armor -= value;
            if (_armor < 0)
            {
                _health -= Mathf.Abs(_armor);
                _armor = 0;
            }
        }
        else
        {
            _health -= value;
        }

        _health = Mathf.Clamp(_health, 0, _maxHealth);

        if (_health < 1) Death();

        _healthBar.SetValue(_health);
        _armorBar.SetValue(_armor);
    }
    public virtual void Heal(int value)
    {
        _health += value;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        _healthBar.SetValue(_health);
    }
    public virtual void AddArmor(int value) 
    {
        _armor += value;
        _armor = Mathf.Clamp(_armor, -100, 20);
        _armorBar.SetValue(_armor);
    }
    public virtual void EndTurn()
    {
        _takenCardsInThisTurn = 0;
        Board.EndTurn();
    }
    public virtual void ClearArmor()
    {
        if (!_keepArmor)
        {
            _armor = 0;
            _armorBar.SetValue(0);
        }
    }

    public abstract void Death();
}
