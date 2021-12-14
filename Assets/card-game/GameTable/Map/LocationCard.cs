using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocationCard : MonoBehaviour
{
    [SerializeField] private TextMesh _cardText;
    [SerializeField] private TextMesh _locationText;
    [SerializeField] private bool _quest;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private SceneAsset _sceneToLoad;

    private bool _isHighlighted;
    [SerializeField] private float _cardLift;
    [SerializeField] private float _cardLiftYAngle;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _opened;

    public void Initialize(bool quest, SceneAsset scene)
    {
        _quest = quest;
        _sceneToLoad = scene;
        _locationText.text = _sceneToLoad.name;

        _cardText.text = _quest ? "?" : "X";

        _startRotation = transform.rotation;
        _startPosition = transform.position;
    }

    private void Update()
    {
        Vector3 lift = _startPosition + Vector3.up * (_isHighlighted ? _cardLift : 0);
        Quaternion angle = Quaternion.Euler(_startRotation.x - _cardLiftYAngle * (_isHighlighted ? 1 : 0), _startRotation.y + _cardLiftYAngle * (_isHighlighted ? 1 : 0), 0);

        if (_opened)
        {
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + Camera.main.transform.forward * .2f, Time.fixedDeltaTime);
            transform.up = Vector3.Lerp(transform.up, Camera.main.transform.forward, Time.fixedDeltaTime);
            FindObjectOfType<Volume>().profile.TryGet<DepthOfField>(out var dof);
            dof.focalLength.value = Mathf.Lerp(dof.focalLength.value, 32, Time.fixedDeltaTime);
            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, lift, Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.fixedDeltaTime);
        }
    }

    private void OnMouseDown()
    {
        _opened = true;

        _dialogue.gameObject.SetActive(true);
        _dialogue._unityEvents[1].AddListener(delegate { Apply(); });

    }

    private void OnMouseEnter()
    {
        _isHighlighted = true;
    }
    private void OnMouseExit()
    {
        _isHighlighted = false;
    }

    public void Apply()
    {
        SceneLoader.LoadScene(_sceneToLoad.name);
    }
}
