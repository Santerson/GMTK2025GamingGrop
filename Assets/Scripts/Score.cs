using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] int phaseThreshold = 10;
    [SerializeField] int phaseIncreaseRate = 10;
    [SerializeField] GameObject phaseIncreaseEffect = null;

    int pastPhaseThresholdCount = 0;

    float score = 0;
    float progressToNextPhase = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        progressToNextPhase += Time.deltaTime;
        text.text = "Time: " + (int)score;
        if ((int)progressToNextPhase >= phaseThreshold)
        {
            FindObjectOfType<ObstacleGenerator>().RaisePhase();
            progressToNextPhase = 0;
            phaseThreshold += phaseIncreaseRate;
            Instantiate(phaseIncreaseEffect, Vector2.zero, Quaternion.identity);
        }
    }
}
