using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTutorial : MonoBehaviour
{
    [SerializeField] private bool _noCards;

    [SerializeField] private string _tutorialSuit;

    [SerializeField] private bool _optional;
    [SerializeField] private string _optionalSuit1;
    [SerializeField] private string _optionalSuit2;

    private bool _canBeLoaded = true;

    private void Start()
    { 
        if (!_noCards)
        {
            if (_optional)
            {
                for (int i = 3; i <= 4; i++)
                {
                    FindObjectOfType<Player>().InstantiateCardInDeck($"{i} of " + _optionalSuit1);
                    FindObjectOfType<Player>().TakeCardFromDeck(false);

                    FindObjectOfType<Player>().InstantiateCardInDeck($"{i} of " + _optionalSuit2);
                    FindObjectOfType<Player>().TakeCardFromDeck(false);
                }
            }

            for (int i = 2; i <= 4; i++)
            {
                FindObjectOfType<Player>().InstantiateCardInDeck($"{i} of " + _tutorialSuit);
                FindObjectOfType<Player>().TakeCardFromDeck(false);
            }
        }

        for (int i = 2; i <= 4; i++)
        {
            FindObjectOfType<Player>().InstantiateCardInDeck($"{i} of " + _tutorialSuit);
        }
        for (int i = 2; i <= 4; i++)
        {
            FindObjectOfType<Player>().InstantiateCardInDeck($"{i} of " + _tutorialSuit);
        }

    }

    private void FixedUpdate()
    {
        if (Board.board && Board.board.PlayerTurn == false)
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5 && Settings.Data.OnlyTutorial)
        {
            if (_canBeLoaded)
            {
                SceneLoader.LoadScene(0);
                Settings.Data.OnlyTutorial = false;
                _canBeLoaded = false;
                this.enabled = false;
            }
        }
        else
        {
            if (_canBeLoaded)
            {
                SceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (Board.board)
            Board.board.PlayerTurn = true;

        Settings.Data.FirstTutorialPassed = true;
        Settings.SaveSettings();
    }
}

