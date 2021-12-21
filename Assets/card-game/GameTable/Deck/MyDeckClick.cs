using UnityEngine;

public class MyDeckClick : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    private void OnMouseUp()
    {
        FindObjectOfType<MyDeck>().ShowDeck();

    }

    public void Show(int count)
    {
        string text = $"��� ����� � ���� ������. \n�� ��� {count}...";
        _dialogue.gameObject.SetActive(true);
        _dialogue.ShowString(text);
    }
}
