using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPos;
    Vector3 camPos;
    public float offset;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.transform.position;
        camPos = playerPos;
        camPos.z = transform.position.z;
        transform.position = camPos;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        camPos = Vector3.Lerp(camPos, playerPos, speed * Time.deltaTime);
        if (playerPos.x > (camPos.x + offset))
        {
            camPos.x = playerPos.x - offset;
        }
        if (playerPos.x < (camPos.x - offset))
        {
            camPos.x = playerPos.x + offset;

        }
        if (playerPos.y > (camPos.y + offset))
        {
            camPos.y = playerPos.y - offset;
        }
        if (playerPos.y < (camPos.y - offset))
        {
            camPos.y = playerPos.y + offset;
        }
        camPos.z = transform.position.z;
        transform.position = camPos;
    }
}
