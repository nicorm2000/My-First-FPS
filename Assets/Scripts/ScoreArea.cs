using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public Transform player;
    public Collider designatedArea;

    private int score = 0;
    private int highScore = 0;

    private bool isInDesignatedArea = false;

    private void Start()
    {
        // Load the high score from PlayerPrefs
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player && other.transform.position == designatedArea.bounds.center)
        {
            // Player has entered the designated area
            isInDesignatedArea = true;
            score = 0; // Reset the score
        }
            Debug.Log("entered");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player && other.transform.position == designatedArea.bounds.center)
        {
            // Player has exited the designated area
            isInDesignatedArea = false;

            // Save the current score as the high score
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }
        }
            Debug.Log("exited");
    }

    private void Update()
    {
        if (isInDesignatedArea)
        {
            // Increase the score over time while the player is in the designated area
            score++;
        }
    }
}