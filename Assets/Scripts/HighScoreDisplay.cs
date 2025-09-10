using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    Text highScoreText;

    private void Start()
    {
        highScoreText = GetComponent<Text>();
    }

    private void Update()
    {
        highScoreText.text = "high score " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
