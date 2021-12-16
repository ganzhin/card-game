using System.IO;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Settings.LoadSettings();
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