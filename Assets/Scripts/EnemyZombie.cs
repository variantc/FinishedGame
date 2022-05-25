using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour, References.IMovingObject, References.IEnemyType
{
    public event Action<Vector3> OnReceiveTarget;   // To signal Movement component
    public event Action OnEnemyKilled;
    
    public float setSpeed;                      // Public property to set in inspector - assigned to 
    public float speed { get; set; }            // speed in Start(), below

    References refs;
    
    void Start()
    {
        refs = FindObjectOfType<References>();
        speed = setSpeed;
    }

    void FixedUpdate()
    {
        SetTarget();
    }

    void SetTarget()
    {
        OnReceiveTarget?.Invoke(refs.player.transform.position);
    }

    public void KillEnemy()
    {
        OnEnemyKilled?.Invoke();
        Destroy(this.gameObject);
    }
}
