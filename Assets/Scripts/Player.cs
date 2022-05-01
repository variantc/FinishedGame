using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, References.IMovingObject
{
    public event Action<Vector3> OnReceiveTarget;
    public event Action OnPlayerScore;

    public float setSpeed;                      // Public property to set in inspector - assigned to 
    public float speed { get; set; }            // speed in Start(), below

    //public float moveReactivateSpeed = 1.5f;

    public float bumpForce;

    References refs;
    Movement movement;


    void Start()
    {
        refs = FindObjectOfType<References>();
        movement = GetComponent<Movement>();
        speed = setSpeed;
        refs.mouse3D.OnMouseLeftClick += Mouse3D_OnMouseLeftClick;  // Register function with Mouse3D.OnMouseLeftClick
        refs.mouse3D.OnMouseRightClick += Mouse3D_OnMouseRightClick;  // Register function with Mouse3D.OnMouseRightClick
    }

    private void FixedUpdate()
    {
        //float rbSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        //if (rbSpeed > speed)
        //{
        //    SetMovementActivity(false);
        //}
        //if (rbSpeed < moveReactivateSpeed && rbSpeed > 0)
        //{
        //    GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    SetMovementActivity(true);
        //}
    }

    private void Mouse3D_OnMouseLeftClick(Vector3 clickPos)
    {
        OnReceiveTarget?.Invoke(clickPos);
    }
    private void Mouse3D_OnMouseRightClick(Vector3 clickPos)
    {
        ShootBullet(clickPos);
    }

    void ShootBullet(Vector3 target)
    {
        Bullet bullet = Instantiate(refs.bulletPrefab, this.transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.SetDirection(this.transform.position, target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<Rigidbody>().AddForce((
                this.transform.position - other.transform.position).normalized * bumpForce);
            //movement.enabled = false;
        }
        if (other.gameObject.tag == "Treasure")
        {
            OnPlayerScore?.Invoke();
            Destroy(other.gameObject);
        }
    }

    void SetMovementActivity(bool active)
    {
        movement.enabled = active;
        Debug.Log("movement = " + active);
    }
}