using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //remember to initialize grid in AStarPathfinding when spawning enemy
    public GameObject player;
    private AStarPathfinding pf;
    private float speed;
    private bool moving;
    public float atkCD;
    private float atkTime;
    // Start is called before the first frame update
    void Start()
    {
        atkTime = 1;
        speed = 2.5f;
        moving = true;
        pf = GetComponent<AStarPathfinding>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //try to attack
        attack();

        //if you're moving, move
        if (moving) move();
    }

    void move()
    {
        float step = speed * Time.deltaTime;
        //new position is closer to the first step on the rout to the player
        transform.position = Vector3.MoveTowards(transform.position,pf.WorldPointFromNode(pf.FindPath(transform.position,player.transform.position)[0]),step);
    }

    void attack() {
        //if not attacking and next to player, start
        if ((pf.FindPath(transform.position, player.transform.position).Count == 0) && atkCD <= 0) {
            atkCD = atkTime;
            //stop moving
            moving = false;
            //start attack display
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        //decrement timer
        atkCD -= Time.deltaTime;

        //if done attacking, finish
        if (atkCD <= 0)
        {
            //stop attack display
            GetComponent<SpriteRenderer>().color = Color.white;
            //start moving if far enough
            if (pf.FindPath(transform.position, player.transform.position).Count > 0) moving = true;
        }

    }
}
