using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public UnityEvent[] _unityEvents;

    [SerializeField] private List<string> _strings = new List<string>();
    [SerializeField] private Text _textObject;
    private float _delay = .04f;

    private bool _isShowing;
    private int _currentStringIndex = -1;

    [SerializeField] private bool _cutScene;

    private Quaternion _cameraLockRotation;
    [SerializeField] private float _cameraZoom = 27;

    [SerializeField] private AudioClip _voiceClip;

    private void Start()
    {
        NextString();
    }

    private void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space)) && _isShowing == false)
        {
            NextString();
        }
        if (_currentStringIndex >= 0 && _currentStringIndex < _unityEvents.Length)
        {
            _unityEvents[_currentStringIndex].Invoke();
        }
    }

    public void NextString()
    {
        if (_isShowing) return;

        if (_currentStringIndex == _strings.Count - 1)
        {
            gameObject.SetActive(false);
            _textObject.text = "";
            return;
        }

        _currentStringIndex++;
        StartCoroutine(ShowTextRoutine(_strings[_currentStringIndex]));
    }

    public void ShowString(string text)
    {
        _strings.Clear();
        _strings.Add(text);
        _currentStringIndex = -1;
        NextString();
    }

    public IEnumerator ShowTextRoutine(string text)
    {
        _isShowing = true;

        float timer = 0;
        _textObject.text = string.Empty;
        yield return new WaitForSeconds(.2f);

        foreach (var letter in text)
        {
            timer = 0;

            _textObject.text += letter;

            if (letter != ' ')
            {
                SoundDesign.PlayOneShot(_voiceClip);
            }

            while (timer < (char.IsPunctuation(letter) ? _delay * 3 : _delay))
            {
                timer += Time.unscaledDeltaTime;

                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    _textObject.text = text;
                    goto br;
                }
                yield return null;
            }
        }
        br:
        _textObject.text += "\n<size=16>*click to continue*</size>";
        yield return new WaitForSeconds(.2f);
        _isShowing = false;
    }

    public void LockCamera(Transform target)
    {
        var camera = Camera.main.transform;
        Vector3 relativePos = target.position - camera.position;

        camera.rotation = Quaternion.Lerp(camera.rotation, Quaternion.LookRotation(relativePos, Vector3.up), Time.deltaTime * 2);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _cameraZoom, Time.deltaTime);
    }

    public void SetStrings(string[] strings)
    {
        _strings.Clear();
        _strings.AddRange(strings);
    }
}
