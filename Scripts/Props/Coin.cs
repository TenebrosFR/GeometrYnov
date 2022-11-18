using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public SpriteRenderer sprite;
    [SerializeField] private int index;
    [SerializeField] private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) PlayerTookCoin();
    }
    private void PlayerTookCoin() {
        gameManager.PlayerTookCoin(this);
        sprite.enabled = false;
    }
}
