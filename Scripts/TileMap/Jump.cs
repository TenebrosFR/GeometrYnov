using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) GameManager.GameManagerInstance.GetPlayerInstance().isJumping = true;
    }
}
