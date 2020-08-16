using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public PlayerControlls player;

    private Rigidbody2D rb;

    public int damage;
    public int swingSpeed;
    public int attackSpeed;
    public float followSpeed;

    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlls>();
        followSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(player.gameObject.transform.position * Time.fixedDeltaTime * followSpeed);
    }
}
