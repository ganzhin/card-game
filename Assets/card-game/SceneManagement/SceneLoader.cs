using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    [SerializeField] public static SceneTransitionImage TransitionImage;
    private static float _transitionTime = 1.25f;
    private static bool _isPlaying = false;

    public static void LoadScene(int buildIndex)
    {
        if (_isPlaying == false)
        {
            TransitionImage = Object.FindObjectOfType<SceneTransitionImage>();
            if (TransitionImage)
            {
                Object.FindObjectOfType<MonoBehaviour>().StartCoroutine(LoadSceneRoutine(buildIndex));
            }
        }
    }

    private static IEnumerator LoadSceneRoutine(int buildIndex)
    {
        _isPlaying = true;
        float time = 0;
        while (time < _transitionTime)
        {
            time += Time.deltaTime;
            TransitionImage.color = Color.Lerp(Color.clear, Color.black, time / _transitionTime);
            yield return null;
        }

        SceneManager.LoadScene(buildIndex);
        _isPlaying = false;
        TransitionImage = Object.FindObjectOfType<SceneTransitionImage>();

        while (time > 0)
        {
            time -= Time.deltaTime;
            TransitionImage.color = Color.Lerp(Color.clear, Color.black, time / _transitionTime);
            yield return null;
        }
    }
}
