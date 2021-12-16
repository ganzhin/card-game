using UnityEngine;

[CreateAssetMenu(fileName = "New QuestionMark Option", menuName = "QuestionMark")]
public class QuestionMarkOption : ScriptableObject
{
    public int ChipsReward;
    public Card[] CardsReward;
    public bool Fight;
}