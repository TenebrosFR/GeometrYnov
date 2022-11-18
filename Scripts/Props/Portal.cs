using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string NewState;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) GameManager.PlayerInstance.<PlayerControl>ChangeState(NewState);
    }
}
