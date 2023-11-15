using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float score = 0;
    private int intScore;

    public void UpdateScore(float scoreAdd) //add score then update
    {
        score += scoreAdd;
        intScore = (int)score * 5;
        DisplayScore(intScore);
    }

    public void DisplayScore(int score)
    {
        textComponent.text = score.ToString();
    }

    public int GetScore()
    {
        return intScore;
    }
    //add conditions of score here
}
