using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    private void Start()
    {
        Settings.LoadSettings();
        _continueButton.interactable = File.Exists($"{Application.dataPath}/Save/deck.xml");
    }
    public void NewGame()
    {
        if (Settings.Data.FirstTutorialPassed)
        {
            SceneLoader.LoadScene("MapScene");
            ChipMoney.Clear();
        }
        else
        {
            SceneLoader.LoadScene(1);

        }
        if (File.Exists($"{Application.dataPath}/Save/deck.xml"))
        {
            File.Delete($"{Application.dataPath}/Save/deck.xml");
        }
    }

    public void Continue()
    {
        SceneLoader.LoadScene("MapScene");
    }

    public void Tutorial()
    {
        Settings.Data.OnlyTutorial = true;
        SceneLoader.LoadScene(1);
    }

    public void OpenSettings()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}