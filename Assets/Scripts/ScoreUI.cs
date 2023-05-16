using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] List<ScoreArea> areas;
    [SerializeField] TMP_Text highestScoreUI;
    [SerializeField] TMP_Text scoreUI;

    public void UpdateScoreText(int currentScore, int currentHighScore)
    {
        if (currentScore == 0)
        {
            highestScoreUI.text = "High Score: " + currentHighScore.ToString();
            scoreUI.text = "Score: " + currentScore.ToString();
        }
        else
        {
            if (currentScore > currentHighScore)
            {
                currentHighScore = currentScore;
            }

            highestScoreUI.text = "High Score: " + currentHighScore.ToString();
            scoreUI.text = "Score: " + currentScore.ToString();
        }
    }
}
