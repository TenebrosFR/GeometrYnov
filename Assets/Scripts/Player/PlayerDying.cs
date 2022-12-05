using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDying : MonoBehaviour
{
    [SerializeField] private GameObject explosionAnimation;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private Rigidbody2D rb;
    //Si un élément censer me tuer rentre dans le collider enter je me tue
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) StartCoroutine(Die());
    }
    //La mort
    public IEnumerator Die()
    {
        Instantiate(explosionAnimation, transform.position, Quaternion.identity);
        foreach (var sprite in sprites)
            sprite.enabled = false;
        rb.simulated = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
