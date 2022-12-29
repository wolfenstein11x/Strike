using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    PlayerStatus player;
    Camera minimapCamera;

    private void Start()
    {
        player = FindObjectOfType<PlayerStatus>();
        minimapCamera = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        // if you want the camera to rotate with player too:
        transform.rotation = Quaternion.Euler(90f, player.transform.eulerAngles.y, 0f);
    }

}
