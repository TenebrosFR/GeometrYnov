using UnityEngine;

public class Coin : MonoBehaviour
{
    public SpriteRenderer sprite;
    [SerializeField] public int index;
    [SerializeField] private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.GameManagerInstance;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) PlayerTookCoin();
    }
    private void PlayerTookCoin() {
        GameManager.GameManagerInstance.PlayerTookCoin(this);
        gameObject.SetActive(false);
    }
}
