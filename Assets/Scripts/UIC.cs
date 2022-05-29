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
    public Text gameOverText;
    public Text killsText;

    private void Start()
    {
        refs = FindObjectOfType<References>();
        scoreText.text = "Score: 0";
        killsText.text = "Kills: 0";
        shotsText.text = "Shots: 0";
        refs.game.OnUpdatedScore += Game_OnUpdatedScore;
        refs.game.OnUpdatedKills += Game_OnUpdatedKills;
        refs.player.GetComponent<Shooting>().OnShotNumberChanged += Player_OnShotNumberChanged;
        refs.player.OnPlayerKilled += Player_OnPlayerKilled;
    }

    private void Player_OnPlayerKilled()
    {
        gameOverText.text += "GAME OVER!";
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
        scoreText.text = "Score: " + score;
    }

    private void Game_OnUpdatedKills(int kills)
    {
        killsText.text = "Kills: " + kills;
    }
    
}

