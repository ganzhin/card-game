using UnityEngine;

public class DeskBell : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;

    private void OnMouseDown()
    {
        if (Board.board.PlayerTurn && Board.board.Cards.Count > 0)
        {
            Board.board.PlayCards();
            SoundDesign.SoundOneShot(_sound);
        }
    }
}