using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GameManager : MonoBehaviour {
    [SerializeField] private int currentLevel = 1;
    private static GameManager _GameManagerInstance;
    public static GameManager GameManagerInstance { get { return _GameManagerInstance; } }
    //ui
    [SerializeField] private Canvas Pause;
    [SerializeField] private Slider LevelProgression;
    [SerializeField] private TextMeshProUGUI Attempt;
    //
    [SerializeField] private PlayerControl playerPrefab;
    [SerializeField] public List<Coin> tookCoin;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RawImage background;
    [SerializeField] private Tilemap ground;
    [SerializeField] private float startOffSet = 0.8f;
    [SerializeField] private Vector3 PlayerStart = new Vector3(0, 0, 0);
    [SerializeField] public Camera Camera;
    [SerializeField] public int totalJump;
    private DateTime StartTime;
    //EndGameResult
    public string[] Result() {
        string seconds = (DateTime.Now - StartTime).Seconds < 10 ? "0"+(DateTime.Now - StartTime).Seconds : (DateTime.Now - StartTime).Seconds.ToString();
        string formatedTime = ((DateTime.Now - StartTime).Seconds / 60).ToString() + " : " +seconds ;
        string[] resultData = { tookCoin.Count.ToString(), totalJump.ToString(), formatedTime };
        return resultData; 
    }
    public float offsetY = 0;
    private bool isPaused = false;
    private int attemptCount = 0;
    //Player
    private PlayerControl Playerinstance;
    public PlayerControl GetPlayerInstance() { return Playerinstance; }
    void Awake()
    {
        //Je passe dans ce if si j'ai fini une partie que je retourne au menu principal et que je reviens
        if (!isPaused && Time.timeScale == 0) Time.timeScale = 1;
        _GameManagerInstance = this;
        audioSource.Play();
    }
    private void OnEnable() {
        StartTime = DateTime.Now;
    }
    private void FixedUpdate() {
        if(ground)ground.color = Color.Lerp(ground.color, Color.red, Mathf.PingPong(Time.time, 60) / 75000);
        if(background)background.color = Color.Lerp(background.color, Color.red, Mathf.PingPong(Time.time, 60) / 75000);
        //Si je n'est pas de player je vais en recrée un
        if (!Playerinstance) restartCurrentLevel();
        //Je suit le joueur
        var currentPlayerX = Playerinstance.transform.position.x;
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentPlayerX + 4, 2.7f + offsetY, -10),0.1f);
        if (LevelProgression) {
            //Calcul de la progression par rapport a la longueur du niveau
            LevelProgression.value = Mathf.Min((currentPlayerX * 100) / 904, 100);
            if (ApplicationData.levels[currentLevel].normalCompletionPercent < Mathf.Round(LevelProgression.value)) ApplicationData.levels[currentLevel].normalCompletionPercent = Mathf.Round(LevelProgression.value);
        }
    }
    //Réinitialise le niveau en cour
    public void restartCurrentLevel(bool resetTimer = false) {
        if (resetTimer) StartTime = DateTime.Now;
        if (isPaused) SwitchPause();
        transform.position = new Vector3(4, 2.7f, -10);
        respawnPlayer();
        respawnCoins();
        if (ground) ground.color = Color.blue;
        if (background) background.color = Color.blue;
        offsetY = 0;
        attemptCount++;
        if(Attempt)Attempt.text = "Attempt " + attemptCount;
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
        Playerinstance = Instantiate(playerPrefab, PlayerStart, Quaternion.identity,transform.parent) ;
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (!context.performed) return;
        SwitchPause();
    }
    public void SwitchPause() {
        isPaused = !isPaused;
        if (isPaused) {
            Time.timeScale = 0;
            audioSource.Pause();
            Pause.gameObject.SetActive(true);
        }
        else {
            Time.timeScale = 1;
            audioSource.Play();
            Pause.gameObject.SetActive(false);
        }
    }
    public void PlayerTookCoin(Coin newTakenCoin) {
        if(!tookCoin.Contains(newTakenCoin)) tookCoin.Add(newTakenCoin);
    }

    public void ChangeScene() {
        SceneManager.LoadScene("MainMenu");
    }
}
