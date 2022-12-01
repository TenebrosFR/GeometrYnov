using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager _GameManagerInstance;
    public static GameManager GameManagerInstance { get { return _GameManagerInstance; } }
    //ui
    [SerializeField] private Slider LevelProgression;
    [SerializeField] private TextMeshProUGUI Attempt;
    //
    [SerializeField] private PlayerControl playerPrefab;
    [SerializeField] public List<Coin> tookCoin;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RawImage background;
    [SerializeField] private float startOffSet = 0.8f;
    [SerializeField] private Vector3 PlayerStart = new Vector3(0, 0, 0);
    [SerializeField] public Camera Camera;
    [SerializeField] public int totalJump;
    private DateTime StartTime;
    //EndGameResult
    public string[] Result() {
        string seconds = (DateTime.Now - StartTime).Seconds < 10 ? "0"+(DateTime.Now - StartTime).Seconds : (DateTime.Now - StartTime).Seconds.ToString();
        string formatedTime = ((DateTime.Now - StartTime).Seconds / 60).ToString() + " : " +seconds ;
        string[] array = { tookCoin.Count.ToString(), totalJump.ToString(), formatedTime };
        return array ; 
    }
    public float offsetY = 0;
    private bool isPaused = false;
    private int attemptCount = 0;
    //Player
    private PlayerControl Playerinstance;
    public PlayerControl GetPlayerInstance() { return Playerinstance; }
    void Awake()
    {
        _GameManagerInstance = this;
        audioSource.Play();
    }
    private void OnEnable() {
        StartTime = DateTime.Now;
    }
    private void FixedUpdate()
    {
        //Si je n'est pas de player je vais en recrée un
        if (!Playerinstance) restartCurrentLevel();
        //Je suit le joueur
        var currentPlayerX = Playerinstance.transform.position.x;
        gameObject.transform.position = new Vector3(currentPlayerX + 4,2.7f+offsetY,-10);
        //Calcul de la progression par rapport a la longueur du niveau
        LevelProgression.value = Mathf.Min((currentPlayerX*100)/904,100);
    }
    //Réinitialise le niveau en cour
    public void restartCurrentLevel(bool resetTimer = false) {
        if (resetTimer) StartTime = DateTime.Now;
        if (!isPaused && Time.timeScale == 0) Time.timeScale = 1;
        respawnPlayer();
        respawnCoins();
        offsetY = 0;
        attemptCount++;
        Attempt.text = "Attempt " + attemptCount;
        Camera.fieldOfView = 65;

    }
    //Refait apparaitre les pièces
    private void respawnCoins() {
        foreach (Coin coin in tookCoin)
            coin.gameObject.SetActive(true);
        tookCoin.Clear();
    }

    //Je rajoute 1 au compteur de mort puis respawn un player et garde la référence de sont transform
    private void respawnPlayer()
    {
        //Quand on fini le niveau et qu'on veut rejouer
        if (Playerinstance) Destroy(Playerinstance.gameObject);
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
        if(!tookCoin.Contains(newTakenCoin)) tookCoin.Add(newTakenCoin);
    }

    public void ChangeScene() {
        SceneManager.LoadScene("MainMenu");
    }
}
