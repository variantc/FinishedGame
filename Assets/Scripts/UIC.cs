using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIC : MonoBehaviour
{
    // We want this to be register after Game Object
    References refs;
    public Text scoreText;
    public Text shotsText;

    private void Start()
    {
        refs = FindObjectOfType<References>();
        scoreText.text = "Score: 0";
        shotsText.text = "Shots: 0";
        refs.game.OnUpdatedScore += Game_OnUpdatedScore;
        refs.player.GetComponent<Shooting>().OnShotNumberChanged += Player_OnShotNumberChanged;
    }

    private void Player_OnShotNumberChanged(int shots)
    {
        UpdateShotText(shots);
    }
    void UpdateShotText(int shots)
    {
        shotsText.text = "Shots: " + shots;
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

