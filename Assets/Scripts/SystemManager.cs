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
        scoreCounter = GameObject.FindWithTag("Score").GetComponent<ScoreCounter>();;
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

    private ScoreCounter scoreCounter;
    private const int scoreModifier = 2;

    private void Start(){ //maybe change to find through instances
        progBar.SetTotalHeight(totalHeight);
    }

    private void Update()
    {
        playerPosition = player.transform.position.y;
        currentHeight = (int)Math.Round(playerPosition / LevelGenerator.instance.cloudSpacing);
        progBar.UpdateProgress(playerPosition); //move to game over after fix
        if (isGameOver == false)
            CheckHeight();
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

}
