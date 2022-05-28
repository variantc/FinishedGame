using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletForce;

    float destroyTime = 4f;
    float destroyClock = 0f;

    private void FixedUpdate()
    {
        destroyClock += Time.deltaTime;
        if (destroyClock >= destroyTime)
            Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 startVec, Vector3 targetVec)
    {
        Vector3 direction = (targetVec - startVec).normalized;
        GetComponent<Rigidbody>().AddForce(bulletForce * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        // this Bullet should be a child of the Shooter's gameObject.transform
        // Therefore, only care if it is not this collider
        if (this.transform.parent == other.gameObject.transform) { return; }

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<References.IEnemyType>().KillEnemy();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);   // since the other GO is Bullet too, both die
            // CARE - Any other functionality here will be called twice
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
        }
    }

    void SetLayer()
    {

    }
}
