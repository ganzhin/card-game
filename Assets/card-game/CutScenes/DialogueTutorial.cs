using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTutorial : MonoBehaviour
{
    [SerializeField] private bool _noCards;

    [SerializeField] private Suit _tutorialSuit;

    [SerializeField] private bool _optional;
    [SerializeField] private Suit _optionalSuit1;
    [SerializeField] private Suit _optionalSuit2;

    private void Awake()
    {
        if (!_noCards)
        {
            if (_optional)
            {
                for (int i = 3; i <= 4; i++)
                {
                    FindObjectOfType<Player>().InstantiateCardInDeck(i, _optionalSuit1);
                    FindObjectOfType<Player>().TakeCardFromDeck();
                    FindObjectOfType<Player>()._takenCardsInThisTurn = 0;

                    FindObjectOfType<Player>().InstantiateCardInDeck(i, _optionalSuit2);
                    FindObjectOfType<Player>().TakeCardFromDeck();
                    FindObjectOfType<Player>()._takenCardsInThisTurn = 0;
                }
            }

            for (int i = 2; i <= 4; i++)
            {
                FindObjectOfType<Player>().InstantiateCardInDeck(i, _tutorialSuit);
                FindObjectOfType<Player>().TakeCardFromDeck();
                FindObjectOfType<Player>()._takenCardsInThisTurn = 0;
            }
        }

        for (int i = 2; i < 9; i++)
        {
            FindObjectOfType<Player>().InstantiateCardInDeck(i, _tutorialSuit);
        }
        for (int i = 2; i < 9; i++)
        {
            FindObjectOfType<Player>().InstantiateCardInDeck(i, _tutorialSuit);
        }

    }
    private void FixedUpdate()
    {
        if (Board.PlayerTurn == false)
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Board.PlayerTurn = true;
    }
}

