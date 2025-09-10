using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float timeBeforeLoading = 2f;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<Game>().ResetGame();
    }
    
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(timeBeforeLoading);
        int highScore = FindObjectOfType<Game>().GetScore();

        if (highScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        SceneManager.LoadScene("Game over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
