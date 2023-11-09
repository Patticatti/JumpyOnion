using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float camHeight;
    private SystemManager systemManager;

    private void Start()
    {
        systemManager = SystemManager.instance.GetComponent<SystemManager>();
    }

    private void Update()
    {
        camHeight = systemManager.playerPosition;
        transform.position = new Vector3(0f, camHeight, -10f);
    }
}
