using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] TextMeshProUGUI highscoreText = null;
    [SerializeField] int phaseThreshold = 10;
    [SerializeField] int phaseIncreaseRate = 10;
    [SerializeField] GameObject phaseIncreaseEffect = null;
    [SerializeField] LineRenderer progressBar;
    [SerializeField] AudioSource nextStageSFX;
    [SerializeField] AudioSource newHighscoreSFX;

    int pastPhaseThresholdCount = 0;
    public float score = 0;
    float progressToNextPhase = 0;
    public float highscore = 0;
    bool isNewHighscore = false;
    bool isGameOver = false;
    public bool stopCounting = false;

    void Start()
    {
        highscore = PlayerPrefs.GetFloat("Highscore", 0);
        UpdateHighscoreText();
    }

    void Update()
    {
        if (isGameOver || stopCounting) return;

        score += Time.deltaTime;
        progressToNextPhase += Time.deltaTime;
        text.text = "Time: " + (int)score;

        if (score > highscore)
        {
            if (!isNewHighscore)
            {
                isNewHighscore = true;
                if (newHighscoreSFX != null)
                {
                    newHighscoreSFX.Play();
                }
                Debug.Log("NEW HIGHSCORE! Current: " + (int)score);
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

    // Call this method when the player dies
    public void OnPlayerDeath()
    {
        isGameOver = true;

        if (isNewHighscore)
        {
            Debug.Log("GAME OVER! NEW HIGHSCORE ACHIEVED: " + (int)highscore);
        }
        else
        {
            Debug.Log("GAME OVER! Time survived: " + (int)score +
                     " | Current Highscore: " + (int)highscore);
        }

        PlayerPrefs.SetFloat("Highscore", highscore);
        PlayerPrefs.Save();
    }

    void UpdateHighscoreText()
    {
        if (highscoreText != null)
        {
            highscoreText.text = "Best: " + (int)highscore;
        }
    }
}