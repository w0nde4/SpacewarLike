using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    Game game;

    private void Start()
    {
        scoreText = GetComponent<Text>();
        game = FindObjectOfType<Game>();
    }

    private void Update()
    {
        scoreText.text = game.GetScore().ToString();
    }
}
