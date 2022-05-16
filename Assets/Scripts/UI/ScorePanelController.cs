using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanelController : MonoBehaviour
{
    [SerializeField] private DamagerScorePanel damagerScorePrefab;
    [SerializeField] private RectTransform spawnPoint;

    private List<Damager> damagers;
    private Dictionary<Damager, DamagerScorePanel> damagerScoreDictionary;

    public void Start()
    {
        damagers = new List<Damager>(FindObjectsOfType<Damager>());
        damagerScoreDictionary = new Dictionary<Damager, DamagerScorePanel>();
        
        foreach (Damager damager in damagers)
        {
            DamagerScorePanel scorePanel = Instantiate(damagerScorePrefab, spawnPoint);
            scorePanel.Init(damager.GetName());
            damager.hitOther += scorePanel.UpdateScore;
            damagerScoreDictionary.Add(damager, scorePanel);
        }
    }

    private void OnDisable()
    {
        foreach (KeyValuePair<Damager, DamagerScorePanel> pair in damagerScoreDictionary)
        {
            Damager damager = pair.Key;
            DamagerScorePanel scorePanel = pair.Value;
            damager.hitOther -= scorePanel.UpdateScore;
        }
    }
}
