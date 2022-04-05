using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControllerForEndScenes : MonoBehaviour
{
    [SerializeField] private OverallScoreSO OverallScore;
    [SerializeField] TextMeshProUGUI obtainedScore;

    void Start()
    {
        obtainedScore.text = "Score: " + OverallScore.overallScore.ToString() + "/" + OverallScore.TargetScore.ToString();
    }

}
