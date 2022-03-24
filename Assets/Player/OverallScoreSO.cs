using UnityEngine;

[CreateAssetMenu(fileName ="Overall Score", menuName = "Overall Score", order = 1)]
public class OverallScoreSO : ScriptableObject
{
    public int overallScore = 0;
    public int TargetScore;
}
