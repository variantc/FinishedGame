using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, References.IMovingObject
{
    public event Action<Vector3> OnReceiveTarget;

    public float setSpeed;                      // Public property to set in inspector - assigned to 
    public float speed { get; set; }            // speed in Start(), below

    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = setSpeed;
        SetTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            SetTarget();
            timer = 0f;
        }
    }

    void SetTarget()
    {
        OnReceiveTarget?.Invoke(new Vector3(
                UnityEngine.Random.Range(-13f, 13f),
                0f,
                UnityEngine.Random.Range(-13f, 13f)));
    }
}
