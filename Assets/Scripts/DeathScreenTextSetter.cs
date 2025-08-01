using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreenTextSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI otherHighScoreText;
    [SerializeField] float highscoretextblinkinterval = 1;
    bool newHighScore = false;
    private void Start()
    {
        try
        {
            Score score = FindObjectOfType<Score>();
            GetComponent<TextMeshProUGUI>().text = $"Final time: {(int)score.score}\nFinal Stage: {FindObjectOfType<ObstacleGenerator>().phase}";
            highScoreText.text = "High score: " + (int)score.highscore;
            if (score.score >= score.highscore)
            {
                newHighScore = true;
            }
            
        }
        catch
        {
            Debug.LogError("An error occured in trying to fetch scores");
        }
    }

    float timeLeft = 0.5f;
    private void Update()
    {
        if (newHighScore)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = highscoretextblinkinterval;
                if (otherHighScoreText.gameObject.activeSelf)
                {
                    otherHighScoreText.gameObject.SetActive(false);
                }
                else
                {
                    otherHighScoreText.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            otherHighScoreText.gameObject.SetActive(false);
        }
    }
}
