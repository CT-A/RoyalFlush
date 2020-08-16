using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 camPos = player.transform.position;
        camPos.z = transform.position.z;
        transform.position = camPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = player.transform.position;
        camPos.z = transform.position.z;
        transform.position = camPos;
    }
}
