using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public event Action<int> hitOther;

    [SerializeField] private string damagerName;
    [SerializeField] private int damagerScoreForOneHit = 1;

    private int score;
    
    public void IncreaseScore()
    {
        score += damagerScoreForOneHit;
        hitOther?.Invoke(score);
    }

    public string GetName()
    {
        return damagerName;
    }
}
