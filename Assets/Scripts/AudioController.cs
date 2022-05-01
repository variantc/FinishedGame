using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource collectAudioSource;
    public AudioSource enemyKilledAudioSource;

    References refs;

    private void Start()
    {
        refs = GetComponent<References>();
        refs.game.OnNewEnemySpawned += Game_OnNewEnemySpawned;  // Get informed on new enemy spawn
        refs.game.OnNewTreasureSpawned += Game_OnNewTreasureSpawned;  // Get informed on new treasure spawn
    }

    private void Game_OnNewEnemySpawned(References.IEnemyType enemy)
    {
        enemy.OnEnemyKilled += Enemy_OnEnemyKilled;     // Subscribe to new enemy so play sound when dies
    }

    private void Game_OnNewTreasureSpawned(Treasure treasure)
    {
        treasure.OnTreasureCollected += Treasure_OnTreasureCollected;     // Subscribe to new treasure 
    }                                                                  // so play sound when collected

    private void Enemy_OnEnemyKilled()      // Play death sound on death
    {
        enemyKilledAudioSource.Play();
    }

    private void Treasure_OnTreasureCollected()      // Play collect sound on collect
    {
        collectAudioSource.Play();
    }
}
