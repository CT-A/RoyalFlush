using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : Weapon
{
    public PlayerControlls player;

    private Rigidbody2D rb;

    public int damage;
    public int attackSpeed;
    public float swingSpeed;
    public float followSpeed;
    public float rotateSpeed;

    public bool isAttacking;

    // Start is called before the first frame update
    public override void InstantiateWeapon(PlayerControlls pc)
    {
        rb = GetComponent<Rigidbody2D>();
        player = pc;
        followSpeed = player.moveSpeed;
        rotateSpeed = 5f;
        swingSpeed = 20f;
        player.weapon = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(player.gameObject.transform.position * Time.fixedDeltaTime * followSpeed * 10);

        float angle = Vector2.Angle(Vector2.right, player.movement);
        if (player.movement.y < 0) angle = angle * -1;
        if (player.movement.x != 0 || player.movement.y != 0)
        {
            if(!isAttacking) rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * rotateSpeed));
        }
    }

    public override void Attack(Vector2 mousePos)
    {
        float angle = Vector2.Angle(Vector2.right, mousePos);
        if (mousePos.y < 0) angle = angle * -1;
        angle += 180;
        isAttacking = true;
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * swingSpeed));
        StartCoroutine(attackAnim());
    }

    IEnumerator attackAnim()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
