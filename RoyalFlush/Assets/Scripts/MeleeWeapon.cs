using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        float angle = Vector2.Angle(Vector2.right, player.movement);
        if (player.movement.y < 0) angle = angle * -1;
        rb.MovePosition(player.gameObject.transform.position * Time.fixedDeltaTime * followSpeed *10);
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime*5f));
    }
}
