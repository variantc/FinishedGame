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
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
