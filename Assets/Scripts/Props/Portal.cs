using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string NewState;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) GameManager.GameManagerInstance.GetPlayerInstance().ChangeState(NewState);
    }
}
