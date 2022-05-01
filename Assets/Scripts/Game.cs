using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public event Action<int> OnUpdatedScore;
    public event Action<Enemy> OnNewEnemySpawned;
    public event Action<Treasure> OnNewTreasureSpawned;

    References refs;

    public float enemySpawnTime;
    public float treasureSpawnTime;
    float enemySpawnClock = 0f;
    float treasureSpawnClock = 0f;

    public Transform[] enemySpawnPosArray;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        refs = GetComponent<References>();
        refs.player.OnPlayerScore += Player_OnPlayerScore;
        SpawnTreasure();
    }

    private void Player_OnPlayerScore()
    {
        score++;
        OnUpdatedScore?.Invoke(score);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemySpawnClock += Time.deltaTime;
        treasureSpawnClock += Time.deltaTime;

        if (enemySpawnClock >= enemySpawnTime)
        {
            SpawnEnemy();
            enemySpawnClock = 0f;
        }
        if (treasureSpawnClock >= treasureSpawnTime)
        {
            SpawnTreasure();
            treasureSpawnClock = 0f;
        }
    }

    void SpawnEnemy()
    {
        Enemy enemy = Instantiate(
            refs.enemyPrefab, 
            enemySpawnPosArray[UnityEngine.Random.Range(0, enemySpawnPosArray.Length)]
            ).GetComponent<Enemy>();

        OnNewEnemySpawned?.Invoke(enemy);
    }

    void SpawnTreasure()
    {
        Treasure treasure = Instantiate(
            refs.treasurePrefab, 
            new Vector3(
                UnityEngine.Random.Range(-13f, 13f),
                0f,
                UnityEngine.Random.Range(-13f, 13f)), 
            Quaternion.identity
            ).GetComponent<Treasure>();

        OnNewTreasureSpawned?.Invoke(treasure);
    }
}
