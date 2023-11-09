using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private RectTransform rectT;
    private int screenWidth;
    private int screenHeight;


    private void Start()
    {
        rectT = GetComponent<RectTransform>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        //y = 100
        //x = 500
        rectT.width = screenHeight - 100;
        rectT.height = 100;
        //SetInsetAndSizeFromParentEdge(Parent.GetComponent<RectTransform>().Left, 50, )
    }
}
