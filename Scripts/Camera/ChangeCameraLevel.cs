using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraLevel : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float offset = 0;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) gameManager.offsetY += offset;
    }
}
