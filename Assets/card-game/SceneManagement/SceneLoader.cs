using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    [SerializeField] public static SceneTransitionImage TransitionImage;
    private static float _transitionTime = .5f;
    private static bool _isPlaying = false;

    public static void LoadScene(int buildIndex)
    {
        if (_isPlaying == false)
        {
            TransitionImage = Object.FindObjectOfType<SceneTransitionImage>();
            if (TransitionImage)
            {
                Object.FindObjectOfType<SceneTransitionImage>().StartCoroutine(LoadSceneRoutine(buildIndex));
            }
        }
    }

    public static void LoadScene(string sceneName)
    {
        if (_isPlaying == false)
        {
            TransitionImage = Object.FindObjectOfType<SceneTransitionImage>();
            if (TransitionImage)
            {
                Object.FindObjectOfType<SceneTransitionImage>().StartCoroutine(LoadSceneRoutine(null, sceneName));
            }
        }
    }

    private static IEnumerator LoadSceneRoutine(int? buildIndex = null, string sceneName = null)
    {
        Object.DontDestroyOnLoad(TransitionImage.gameObject.transform.parent.gameObject);
        _isPlaying = true;
        float time = 0;
        while (time < _transitionTime)
        {
            time += Time.deltaTime;
            TransitionImage.color = Color.Lerp(Color.clear, Color.black, time / _transitionTime);
            yield return null;
        }
        if (buildIndex != null)
        {
            SceneManager.LoadScene((int)buildIndex);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

        _isPlaying = false;

        while (time > 0)
        {
            time -= Time.deltaTime;
            TransitionImage.color = Color.Lerp(Color.clear, Color.black, time / _transitionTime);
            yield return null;
        }
        Object.Destroy(TransitionImage.gameObject.transform.parent.gameObject);
    }
}
