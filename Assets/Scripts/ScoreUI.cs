using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text highestScore;
    [SerializeField] TMP_Text scoreUI;

    private int currentScore = 0;

    public void UpdateScoreText(int score)
    {
        if (score <= 0)
        {
            scoreUI.text = "";
        }
        else
        {
            if (score > currentScore)
            {
                scoreUI.text = highestScore.ToString();
            }

            scoreUI.text = score.ToString();
        }
    }
}
