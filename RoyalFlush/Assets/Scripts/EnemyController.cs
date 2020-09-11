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
    private float attackRange;
    private Rigidbody2D rb;
    private float lungeSpeed;
    public float lungeTimer;
    private float lungeTime;
    private float accuracy;
    private Vector3 targetPos;
    private bool attacking;
    public float hp;
    public int maxHP;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackRange = 1.5f;
        atkTime = .8f;
        speed = 2.5f;
        lungeSpeed = 10;
        lungeTimer = 0;
        lungeTime = .6f;
        attacking = false;
        //accuracy is how close to lunging we keep adjusting targeting, closer to 0 is better
        accuracy = .1f;
        moving = true;
        pf = GetComponent<AStarPathfinding>();
        player = GameObject.FindWithTag("Player");
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().numEnemies += 1;
        //remember to change this
        maxHP = 10;
        hp = maxHP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if dead, die
        if (hp < 0) die();

        //try to attack
        attack();

        //if you're moving, move
        if (moving) move();
    }

    void die()
    {
        GameManager gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gm.drop(gameObject.transform.position);
        gm.numEnemies -= 1;
        gm.death(transform.position,2);
        Destroy(gameObject);
    }

    void move()
    {
        //new position is closer to the first step on the rout to the player
        //so find this step
        Vector3 stepOne = pf.WorldPointFromNode(pf.FindPath(transform.position, player.transform.position)[0]);
        stepOne.z = 0;
        //dir to target
        Vector3 towardsPlayer = (stepOne - transform.position).normalized;
        rb.velocity = (towardsPlayer*speed);
    }

    void attack() {
        //if not attacking and next to player, start
        if (((transform.position - player.transform.position).magnitude <= attackRange) && atkCD <= 0) {
            atkCD = atkTime;
            //stop moving
            //if (pf.FindPath(transform.position, player.transform.position).Count == 0)
            moving = false;

            //start attack display
            GetComponent<SpriteRenderer>().color = Color.red;
            attacking = true;
        }

        //decrement timers
        atkCD -= Time.deltaTime;
        lungeTimer -= Time.deltaTime;

        //if not locked on, target
        //accuracy is how close to lunging we keep adjusting targeting
        if (atkCD >= accuracy)
            targetPos = player.transform.position;

        //if done charging, finish attacking
        if (atkCD <= 0 && lungeTimer <= 0)
        {
            //If params are right
            if (/*(transform.position - player.transform.position).magnitude <= attackRange*/attacking) {
                Vector3 towardsTarget = (targetPos - transform.position).normalized;
                rb.velocity = (towardsTarget * lungeSpeed);
                lungeTimer = lungeTime;
                atkCD = atkTime;
                attacking = false;
            }

            //stop attack display
            GetComponent<SpriteRenderer>().color = Color.white;
            //start moving if far enough
            if ((pf.FindPath(transform.position, player.transform.position).Count > 0) && lungeTimer <= 0) moving = true;
        }

    }
}
