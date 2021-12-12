using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneLoader.LoadScene(1);
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