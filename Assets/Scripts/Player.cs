using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, References.IMovingObject, References.IShootingObject
{
    public event Action<Vector3> OnReceiveTarget;
    public event Action<Vector3> OnShootToTarget;
    public event Action OnPlayerKilled;
    public event Action OnTreasurePickup;

    public float setSpeed;                      // Public property to set in inspector - assigned to 
    public float speed { get; set; }            // speed in Start(), below

    public float bumpForce;

    References refs;
    Movement movement;
    Shooting shooting;
    
    AudioSource collectAudioSource;

    void Start()
    {
        refs = FindObjectOfType<References>();
        movement = GetComponent<Movement>();
        shooting = GetComponent<Shooting>();
        speed = setSpeed;
        refs.mouse3D.OnMouseLeftClick += Mouse3D_OnMouseLeftClick;  // Register function with Mouse3D.OnMouseLeftClick
        refs.mouse3D.OnMouseRightClick += Mouse3D_OnMouseRightClick;  // Register function with Mouse3D.OnMouseRightClick

        collectAudioSource = GetComponent<AudioSource>();
    }

    private void Mouse3D_OnMouseLeftClick(Vector3 clickPos)
    {
        OnReceiveTarget?.Invoke(clickPos);
    }
    private void Mouse3D_OnMouseRightClick(Vector3 clickPos)
    {
        OnShootToTarget?.Invoke(clickPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Rigidbody>().AddForce((
                this.transform.position - other.transform.position).normalized * bumpForce);
        }
        if (other.gameObject.tag == "Treasure")
        {
            OnTreasurePickup?.Invoke();
            other.gameObject.GetComponent<Treasure>().CollectTreasure();
        }
        if (other.gameObject.tag == "Destroyer")
        {
            OnPlayerKilled?.Invoke();
            Destroy(this.gameObject);
        }
    }
}