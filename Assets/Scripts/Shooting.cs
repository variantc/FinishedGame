using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public event Action<int> OnShotNumberChanged;

    References refs;
    References.IShootingObject shootinGameObject;

    public int shots = 0;
    int reloadAmount = 5;

    void Start()
    {
        refs = FindObjectOfType<References>();
        shootinGameObject = GetComponent<References.IShootingObject>();
        shootinGameObject.OnShootToTarget += ShootinGameObject_OnReceiveTarget;
        // If this shooting component is on a Player, add to the treasure pickup action
        if (this.gameObject.GetComponent<Player>() == true)
            this.gameObject.GetComponent<Player>().OnTreasurePickup += Shooting_OnTreasurePickup;
    }

    private void Shooting_OnTreasurePickup()
    {
        ChangeShotsLeft(reloadAmount);
    }

    void ChangeShotsLeft(int shotChange)
    {
        shots += shotChange;
        OnShotNumberChanged?.Invoke(shots);
    }

    private void ShootinGameObject_OnReceiveTarget(Vector3 obj)
    {
        if (shots > 0)
        {
            Bullet bullet = Instantiate(refs.bulletPrefab, this.transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.SetDirection(this.transform.position, obj);
            ChangeShotsLeft(-1);
        }
    }
}