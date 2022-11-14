using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDying : MonoBehaviour
{
    //Si un élément censer me tuer rentre dans le collider enter je me tue
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) Die();
    }
    //Je meur (actuellement ne fait qu'un destroy
    public void Die()
    {
        Destroy(gameObject);
    }
}
