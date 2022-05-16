using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagerScorePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damagerName;
    [SerializeField] private TextMeshProUGUI currentScore;

    public void Init(string damagerName)
    {
        this.damagerName.text = damagerName;
    }

    public void UpdateScore(int score)
    {
        currentScore.text = score.ToString();
    }
}
