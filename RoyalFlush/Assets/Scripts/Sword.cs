using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sword : Weapon
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
        if (!isAttacking) rb.MovePosition(player.gameObject.transform.position * Time.fixedDeltaTime * followSpeed * 10);

        float angle = Vector2.Angle(Vector2.right, player.mousePos);
        if (player.mousePos.y < 0) angle = angle * -1;
        angle += 180;
        isAttacking = true;
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * swingSpeed));
    }

    public override void Attack(Vector2 mousePos)
    {
        isAttacking = true;
        rb.MovePosition((player.gameObject.transform.position + new Vector3(mousePos.x,mousePos.y,0)*10) * Time.fixedDeltaTime );
        StartCoroutine(attackAnim());
    }

    IEnumerator attackAnim()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
    }
}
