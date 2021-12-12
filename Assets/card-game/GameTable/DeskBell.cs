using UnityEngine;

public class DeskBell : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (Board.PlayerTurn && Board.board.Cards.Count > 0)
        {
            Board.board.PlayCards();
        }
    }
}