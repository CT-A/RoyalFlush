using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sword : Weapon
{
    public PlayerControlls player;

    private Rigidbody2D rb;

    public float offsetDistance;
    public int damage;
    public int attackSpeed;
    public float swingSpeed;
    public float followSpeed;
    public float rotateSpeed;
    public bool isAttacking;

    // Start is called before the first frame update
    public override void InstantiateWeapon(PlayerControlls pc)
    {
        Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        rb = GetComponent<Rigidbody2D>();
        player = pc;
        followSpeed = player.moveSpeed;
        rotateSpeed = 5f;
        swingSpeed = 20f;
        player.weapon = this;
        isAttacking = false;
        offsetDistance = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        float angle = Vector2.Angle(Vector2.right, player.mousePos);
        if (player.mousePos.y < 0) angle = angle * -1;
        angle += 180;
        //Debug.Log("rb is " + rb);
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * swingSpeed));

        Vector3 offsetVec = new Vector3(player.mousePos.x, player.mousePos.y, 0).normalized * offsetDistance;
        //offsetVec = Quaternion.Euler(0, 0, angle) * offsetVec;
        rb.MovePosition((player.gameObject.transform.position + offsetVec) * Time.fixedDeltaTime * followSpeed * 10);
    }

    public override void Attack(Vector2 mousePos)
    {
        isAttacking = true;
        offsetDistance = 2;
        //rb.MovePosition((player.gameObject.transform.position + new Vector3(mousePos.x,mousePos.y,0).normalized*10) * Time.fixedDeltaTime );
        
        StartCoroutine(attackAnim());
    }

    IEnumerator attackAnim()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
        offsetDistance = 1;
    }
}
