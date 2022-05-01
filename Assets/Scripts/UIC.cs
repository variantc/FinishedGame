using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIC : MonoBehaviour
{
    // We want this to be register after Game Object
    References refs;
    public Text scoreText;

    private void Start()
    {
        refs = FindObjectOfType<References>();
        scoreText.text = "Score: 0";
        refs.game.OnUpdatedScore += Game_OnUpdatedScore;
    }

    private void Game_OnUpdatedScore(int score)
    {
        UpdateScoreText(score);
    }

    void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
}

