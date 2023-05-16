using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] Button button;

    public void Pause()
    {
        if (gameObject.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        button.Select();
        gameObject.SetActive(true);
    }


    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

