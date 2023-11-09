using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightCounter : MonoBehaviour //returns height as float
{
    public float height = 0;
    [SerializeField] private Transform player;

    // Update is called once per frame
    private void Update()
    {
        if (player.position.y > height)
            height = player.position.y;
    }
}
