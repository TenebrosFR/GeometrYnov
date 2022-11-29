using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _GameManagerInstance;
    public static GameManager GameManagerInstance { get { return _GameManagerInstance; } }

    [SerializeField] private PlayerControl playerPrefab;
    [SerializeField] private List<Coin> tookCoin;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RawImage background;
    [SerializeField] private float startOffSet = 0.8f;
    [SerializeField] private float cameraOffSet = 0;
    [SerializeField] private Vector3 PlayerStart = new Vector3(0,0,0);
    [SerializeField] public Camera Camera;
    public float offsetY = 0;
    private bool isPaused = false;
    private int deathCount = -1;
    //Player
    private PlayerControl Playerinstance;
    public PlayerControl GetPlayerInstance() { return Playerinstance; }
    void Awake()
    {
        _GameManagerInstance = this;
        audioSource.Play();
    }
    private void FixedUpdate()
    {
        //Si je n'est pas de player je vais en recrée un
        if (!Playerinstance) restartCurrentLevel();
        //Je suit le joueur
        gameObject.transform.position = new Vector3(Playerinstance.transform.position.x+4,2.7f+offsetY,-10);
    }
    //Réinitialise le niveau en cour
    private void restartCurrentLevel() {
        respawnPlayer();
        respawnCoins();
        offsetY = 0;
        deathCount++;
        Camera.fieldOfView = 65;

    }
    //Refait apparaitre les pièces
    private void respawnCoins() {
        foreach (Coin coin in tookCoin)
            coin.sprite.enabled = true;
        tookCoin.Clear();
    }

    //Je rajoute 1 au compteur de mort puis respawn un player et garde la référence de sont transform
    private void respawnPlayer()
    {
        audioSource.Play();
        background.uvRect = new Rect(new Vector2(background.uvRect.size.x*startOffSet, 0),background.uvRect.size);
        Playerinstance = Instantiate(playerPrefab, PlayerStart, Quaternion.identity);
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (!context.performed) return;
        isPaused = !isPaused;
        if (isPaused) {
            Time.timeScale = 0;
            audioSource.Pause();
        }
        else {
            Time.timeScale = 1;
            audioSource.Play();
        }
        
    }

    public void PlayerTookCoin(Coin newTakenCoin) {
        tookCoin.Add(newTakenCoin);
    }
}
