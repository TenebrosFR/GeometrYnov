using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<Coin> tookCoin;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RawImage background;
    private bool isPaused = false;
    private int deathCount = 0;
    void Awake()
    {
        _instance = this;
        audioSource.Play();
    }
    private void FixedUpdate()
    {
        //Si je n'est pas de player je vais en recrée un
        if (!player) restartCurrentLevel();
        //Je suit le joueur
        gameObject.transform.position = new Vector3(player.transform.position.x+4,2.5f,-10);
    }
    //Réinitialise le niveau en cour
    private void restartCurrentLevel() {
        respawnPlayer();
        respawnCoins();
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

    public void PlayerTookCoin(Coin newTakenCoin) {
        tookCoin.Add(newTakenCoin);
    }
}
