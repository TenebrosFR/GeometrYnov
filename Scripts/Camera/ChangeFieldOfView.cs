using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFieldOfView : MonoBehaviour
{
    private Camera _camera;
    private void Start() {
        _camera = GameManager.GameManagerInstance.Camera;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        //Zoom quand on rentre
        if (collision.gameObject.layer == 3) _camera.fieldOfView = 50;
    }
}
