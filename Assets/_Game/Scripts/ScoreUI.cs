using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : Singleton<ScoreUI>
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    private void Update()
    {
        scoreTMP.text = TargetSpawner.Instance.targetKillCount.ToString();
    }
}
