using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraLevel : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float offset = 0;

    private void Start() {
        gameManager = GameManager.GameManagerInstance;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        //Si le joueur rentré je lui met l'offset programmé
        if (collision.gameObject.layer == 3)    gameManager.offsetY = offset;
    }
}
