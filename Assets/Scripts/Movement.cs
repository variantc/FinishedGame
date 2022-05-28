using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    References refs;
    Vector3 target;
    bool hasTarget = false;
    References.IMovingObject movingGameObject;

    private void Start()
    {
        refs = FindObjectOfType<References>();
        movingGameObject = GetComponent<References.IMovingObject>();
        movingGameObject.OnReceiveTarget += MovingGameObject_OnReceiveTarget;
    }

    private void MovingGameObject_OnReceiveTarget(Vector3 obj)
    {
        hasTarget = true;
        target = obj;
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            DoMove();
        }
    }

    void DoMove()
    {
        if (this.transform.position == target)
        {
            hasTarget = false;
            return;
        }

        this.transform.position = Vector3.MoveTowards(
            movingGameObject.transform.position, 
            target, 
            Time.deltaTime * movingGameObject.speed);
    }
}