using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<Card> Cards = new List<Card>();
    public static Board board => FindObjectOfType<Board>();
    public bool PlayerTurn = true;

    [SerializeField] private Player _player;
    [SerializeField] private Transform[] places;

    [SerializeField] private int _maxEnergy = 9;
    [SerializeField] private int _currentEnergyPrice;
    [SerializeField] private Bar _priceMeter;

    [SerializeField] private bool _victory;
    [SerializeField] private bool _loose;

    private void Start()
    {
        PlayerTurn = true;
        _loose = false;
        _victory = false;
    }

    private void Update()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            if (Cards[i] != null)
            {
                Cards[i].transform.position = Vector3.Lerp(Cards[i].transform.position, places[i].transform.position, Time.fixedDeltaTime * 4);
                Cards[i].transform.rotation = Quaternion.Lerp(Cards[i].transform.rotation, places[i].transform.rotation, Time.fixedDeltaTime * 4);
            }
        }

        
    }

    public void PlayCards()
    {
        if (Cards.Count == 0) return;
        if (_currentEnergyPrice > _maxEnergy) return;

        StartCoroutine(PlayCardsRoutine());
        
    }

    private IEnumerator PlayCardsRoutine()
    {
        foreach (var card in Cards)
        {
            card.GetComponent<Collider>().enabled = false;
        }

        foreach (var card in Cards)
        {
            card.Play();
            yield return new WaitForSeconds(Settings.CardPause);
        }

        Cards.Clear();
        ChangeCurrentPrice(-_currentEnergyPrice);
        EndTurn();
    }

    public static void ChangeCurrentPrice(int value)
    {
        board._currentEnergyPrice += value;
        board._priceMeter.SetValue(board._currentEnergyPrice);
    }

    public void PlaceCard(Card card)
    {
        if (Cards.Count > 4)
        {
            Debug.Log("На столе 5 карт");
            return;
        }
        card.transform.parent = null;

        Cards.Add(card);
        _player.RemoveFromHand(card);
        card.IsOnBoard = true;
        card.PlaceEffect();
    }

    public void RemoveCard(Card card)
    {
        if (!Cards.Contains(card)) return;

        _player.AddInHand(card);
        Cards.Remove(card);
        card.IsOnBoard = false;
        card.RemoveEffect();
    }

    public static void EndTurn()
    {
        board.PlayerTurn = !board.PlayerTurn;
        FindObjectOfType<Player>()._takenCardsInThisTurn = 0;

        if (board.PlayerTurn == false)
        {
            FindObjectOfType<Enemy>().MakeMoves();
        }

        FindObjectOfType<CardShop>().PlaceCards();
        
        if (board._loose || board._victory)
        {
            board.EndGame();
        }
    }

    public void EndGame()
    {
        foreach (var deck in FindObjectsOfType<Deck>())
        {
            deck.RestoreBurnedCards();
        }
        if (_loose)
        {
            SceneLoader.LoadScene(0);
        }
        else if (_victory)
        {
            SceneLoader.LoadScene("MapScene");
        }
        PlayerTurn = false;
        _loose = false;
        _victory = false;
    }
    public void Lose()
    {
        _loose = true;
    }
    public void Win()
    {
        _victory = true;
    }
}