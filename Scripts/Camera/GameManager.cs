using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RawImage background;
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
        gameObject.transform.position = new Vector3(player.position.x,0,-10);
    }

    //Je rajoute 1 au compteur de mort puis respawn un player et garde la référence de sont transform
    private void respawnPlayer()
    {
        deathCount++;
        background.uvRect = new Rect(new Vector2(0, 0),background.uvRect.size);
        player = Instantiate(playerPrefab, new Vector3(0, -0.625f, 0), Quaternion.identity).transform;
        audioSource.Play();
    }
}
