using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    [SerializeField] private string[] _strings;
    [SerializeField] private QuestionMarkOption[] _choices;

    private void Start()
    {
        FindObjectOfType<Dialogue>().SetStrings(_strings);
    }

    public void Option(int index)
    {
        ChipMoney.Floor++;

        if (_choices[index].Fight)
        {
            SceneLoader.LoadScene("GameScene");
        }
        else
        {
            SceneLoader.LoadScene("MapScene");
        }
    }
}