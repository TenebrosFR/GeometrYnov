using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(player.position.x,0,-10);
    }
}
