using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public event Action OnTreasureCollected;

    public void CollectTreasure()
    {
        OnTreasureCollected?.Invoke();
        Destroy(this.gameObject);
    }
}
