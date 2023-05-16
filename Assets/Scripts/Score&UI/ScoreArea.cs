using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public Transform player;
    public Collider designatedArea;
    [SerializeField] List<Enemy> enemies;

    private int score;
    private int highScore;
    private bool isInDesignatedArea = false;

    private void Awake()
    {
        score = 0;
        highScore = 0;
    }

    private void Start()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Init(UpdateScore);
        }
    }

    private void UpdateScore(int addedScore)
    {
        score += addedScore;
        FindObjectOfType<ScoreUI>().UpdateScoreText(score, highScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            isInDesignatedArea = true;
            
            SetActiveEnemies(isInDesignatedArea);

            score = 0; // Reset the score
            FindObjectOfType<ScoreUI>().UpdateScoreText(0, highScore);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isInDesignatedArea = false;

            SetActiveEnemies(isInDesignatedArea);

            // Save the current score as the high score
            if (score > highScore)
            {
                highScore = score;
            }
            FindObjectOfType<ScoreUI>().UpdateScoreText(0, 0);
        }
    }

    private void SetActiveEnemies(bool setActive)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].gameObject.SetActive(isInDesignatedArea);
        }
    }
}