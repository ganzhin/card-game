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

        _choices[index].Apply(); ;
    }
}