using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour, References.IMovingObject, References.IEnemyType, References.IShootingObject
{
    public event Action<Vector3> OnReceiveTarget;   // To signal Movement component
    public event Action<Vector3> OnShootToTarget;
    public event Action OnEnemyKilled;

    public float setSpeed;                      // Public property to set in inspector - assigned to 
    public float speed { get; set; }            // speed in Start(), below

    References refs;

    float shootTime = 1f;
    float shootTimer = 0f;

    void Start()
    {
        refs = FindObjectOfType<References>();
        speed = setSpeed;
    }

    void FixedUpdate()
    {
        SetTarget();
        DoShooting();
    }

    void DoShooting()
    {
        if (shootTimer >= shootTime)
        {
            OnShootToTarget?.Invoke(refs.player.transform.position);
            shootTimer = 0f;
        }
        shootTimer += Time.deltaTime;
    }

    void SetTarget()
    {
        OnReceiveTarget?.Invoke(new Vector3(
                UnityEngine.Random.Range(-13f, 13f),
                0f,
                UnityEngine.Random.Range(-13f, 13f)));
        //OnReceiveTarget?.Invoke(refs.player.transform.position);
    }

    public void KillEnemy()
    {
        OnEnemyKilled?.Invoke();
        Destroy(this.gameObject);
    }
}
