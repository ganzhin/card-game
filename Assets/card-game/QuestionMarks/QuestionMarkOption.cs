using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestionMark Option", menuName = "QuestionMark")]
public class QuestionMarkOption : ScriptableObject
{
    public int ChipsReward = 12;
    public int MaxHealthReward = 1;

    public string _sceneToLoad;

    public void Apply()
    {
        ChipMoney.Money += ChipsReward;
        ChipMoney.MaxHealth += MaxHealthReward;

        SceneLoader.LoadScene(_sceneToLoad);
    }
}