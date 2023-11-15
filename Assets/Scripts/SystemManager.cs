using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour //has all the global shit
{
    #region Singleton
    public static SystemManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        player = GameObject.FindWithTag("Player");
        progressBar = GameObject.FindWithTag("Bar");
        scoreCounter = GameObject.FindWithTag("Score").GetComponent<ScoreCounter>();
        countDownObj = GameObject.FindWithTag("Countdown");
        countDownCounter = countDownObj.GetComponent<ScoreCounter>();
        playerController = player.GetComponent<PlayerController>();
        progBar = progressBar.GetComponent<ProgressBar>();
    }
    #endregion

    public GameObject player;
    public GameObject progressBar;
    public ProgressBar progBar;

    public float height = 0f;
    public int currentHeight;
    public float totalHeight = 20f;
    public float playerPosition;
    public bool isGameOver = false;

    private int countDownTimer = 3;
    private ScoreCounter scoreCounter;
    private ScoreCounter countDownCounter;
    private PlayerController playerController;
    private GameObject countDownObj;
    private const int scoreModifier = 2;
    private bool gameStart = false;

    private void Start()
    {
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        if (gameStart)
        {
            playerController.PlayerUpdate();
            playerPosition = player.transform.position.y;
            currentHeight = (int)Math.Round(playerPosition / LevelGenerator.instance.cloudSpacing);
            progBar.UpdateProgress(playerPosition); //move to game over after fix
            if (isGameOver == false)
                CheckHeight();
        }
    }

    public void AddScore(float score)
    {
        scoreCounter.UpdateScore(score * scoreModifier);
    }

    private void CheckHeight(){

        if (playerPosition > height){
            AddScore(playerPosition - height);
            height = playerPosition;
        }
        // else if (playerPosition < height - 3.0f){ //change to when below loaded zone
        //     Debug.Log("Game Over");
        //     isGameOver = true;
        // }
    }

    private IEnumerator CountDown()
    {
        for (int x = countDownTimer; x > -1; x--)
        {
            countDownCounter.DisplayScore(x);
            Debug.Log("working");
            yield return new WaitForSeconds(1);
        }
        player.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        gameStart = true;
        countDownObj.SetActive(false);
    }

}
