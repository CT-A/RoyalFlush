using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //remember to initialize grid in AStarPathfinding when spawning enemy
    public GameObject player;
    public AStarPathfinding pf;
    public float speed;
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        moving = true;
        pf = GetComponent<AStarPathfinding>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) move();
    }

    void move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,pf.WorldPointFromNode(pf.FindPath(transform.position,player.transform.position)[0]),step);
    }
}
