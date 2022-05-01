using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class References : MonoBehaviour
{
    public Game game;
    public Mouse3D mouse3D;
    public Player player;
    
    public GameObject enemyPrefab;
    public GameObject treasurePrefab;
    public GameObject bulletPrefab;

    private void Awake()
    {
        game = GetComponent<Game>();
        mouse3D = GetComponent<Mouse3D>();
        player = FindObjectOfType<Player>();
    }


    public interface IMovingObject
    {
        event Action<Vector3> OnReceiveTarget;
        Transform transform { get; }
        float speed { get; }
    }
    public interface IShootingObject
    {
        event Action<Vector3> OnReceiveTarget;
        Transform transform { get; }
    }
}
