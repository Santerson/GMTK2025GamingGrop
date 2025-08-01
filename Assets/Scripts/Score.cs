using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] TextMeshProUGUI highscoreText = null; // Optional - can be null
    [SerializeField] int phaseThreshold = 10;
    [SerializeField] int phaseIncreaseRate = 10;
    [SerializeField] GameObject phaseIncreaseEffect = null;
    [SerializeField] LineRenderer progressBar;
    [SerializeField] AudioSource nextStageSFX;
    [SerializeField] AudioSource newHighscoreSFX; // Optional - can be null

    int pastPhaseThresholdCount = 0;
    float score = 0;
    float progressToNextPhase = 0;
    float highscore = 0;
    bool isNewHighscore = false;

    void Start()
    {
        // Load the saved highscore when the game starts
        highscore = PlayerPrefs.GetFloat("Highscore", 0);
        UpdateHighscoreText();
    }

    void Update()
    {
        score += Time.deltaTime;
        progressToNextPhase += Time.deltaTime;
        text.text = "Time: " + (int)score;

        // Check for new highscore during gameplay
        if (score > highscore)
        {
            if (!isNewHighscore)
            {
                isNewHighscore = true;
                if (newHighscoreSFX != null) // Null check for SFX
                {
                    newHighscoreSFX.Play();
                }
            }
            highscore = score;
            UpdateHighscoreText();
        }

        if ((int)progressToNextPhase >= phaseThreshold)
        {
            FindObjectOfType<ObstacleGenerator>().RaisePhase();
            progressToNextPhase = 0;
            phaseThreshold += phaseIncreaseRate;
            Instantiate(phaseIncreaseEffect, Vector2.zero, Quaternion.identity);
            nextStageSFX.Play();
        }

        progressBar.SetPosition(0, new Vector2(-9f, 0f));
        float progress = Mathf.Clamp01(progressToNextPhase / phaseThreshold);
        progressBar.SetPosition(1, new Vector2(Mathf.Lerp(-9, 9, progress), 0));
    }

    void UpdateHighscoreText()
    {
        if (highscoreText != null) // Null check for highscore text
        {
            highscoreText.text = "Best: " + (int)highscore;
        }
    }

    void OnDestroy()
    {
        // Save the highscore when the game ends
        PlayerPrefs.SetFloat("Highscore", highscore);
        PlayerPrefs.Save();
    }
}