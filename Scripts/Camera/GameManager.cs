using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RawImage background;
    private bool isPaused = false;
    private int deathCount = 0;
    void Start()
    {
        audioSource.Play();
    }
    private void FixedUpdate()
    {
        //Si je n'est pas de player je vais en recrée un
        if (!player) respawnPlayer();
        //Je suit le joueur
        gameObject.transform.position = new Vector3(player.transform.position.x+4,2.5f,-10);
    }

    //Je rajoute 1 au compteur de mort puis respawn un player et garde la référence de sont transform
    private void respawnPlayer()
    {
        deathCount++;
        background.uvRect = new Rect(new Vector2(0, 0),background.uvRect.size);
        player = Instantiate(playerPrefab, new Vector3(0, -0.625f, 0), Quaternion.identity);
        audioSource.Play();
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (!context.performed) return;
        isPaused = !isPaused;
        if (isPaused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
