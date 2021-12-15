using System.IO;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneLoader.LoadScene(1);
        if (File.Exists($"{Application.dataPath}/Save/deck.xml"))
        {
            File.Delete($"{Application.dataPath}/Save/deck.xml");
        }
    }

    public void Continue()
    {

    }

    public void Settings()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}