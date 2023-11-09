using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float score = 0;
    private float intScore;

    private void Start(){
        textComponent = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateScore(float scoreAdd) //add score then update
    {
        score += scoreAdd;
        intScore = (int)score;
        textComponent.text = intScore.ToString();
    }
    //add conditions of score here
}
