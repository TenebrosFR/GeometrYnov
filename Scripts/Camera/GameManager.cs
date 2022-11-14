using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerPrefab;
    private int deathCount = 0;
    private void FixedUpdate()
    {
        //Si je n'est pas de player je vais en recrée un
        if (!player) respawnPlayer();
        //Je suit le joueur
        gameObject.transform.position = new Vector3(player.position.x,0,-10);
    }

    //Je rajoute 1 au compteur de mort puis respawn un player et garde la référence de sont transform
    private void respawnPlayer()
    {
        deathCount++;
        player = Instantiate(playerPrefab, new Vector3(0, -0.625f, 0), Quaternion.identity).transform;
    }
}
