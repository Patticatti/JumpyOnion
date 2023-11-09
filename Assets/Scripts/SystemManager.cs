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
    }
    #endregion

    public GameObject player;
    public float height = 0f;
    public float playerPosition;
    public bool isGameOver = false;

    private void Start(){
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        playerPosition = player.transform.position.y;
        if (isGameOver == false)
            CheckHeight();
    }

    private void CheckHeight(){

        if (playerPosition > height)
            height = playerPosition;
        if (playerPosition < height - 3.0f){ //change to when below loaded zone
            Debug.Log("Game Over");
            isGameOver = true;
        }
    }

}
