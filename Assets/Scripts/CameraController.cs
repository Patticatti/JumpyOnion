using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private SystemManager systemManager;

    private void Start()
    {
        systemManager = SystemManager.instance.GetComponent<SystemManager>();
    }

    private void Update()
    {
        transform.position = new Vector3(0f, systemManager.playerPosition + 1f, -10f);
    }
}
