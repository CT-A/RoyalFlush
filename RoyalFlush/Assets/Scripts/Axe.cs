﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Axe : Weapon
{
    public PlayerControlls player;

    private Rigidbody2D rb;

    public float offsetDistance;
    public int damage;
    public float attackSpeed;
    public float swingSpeed;
    public float followSpeed;
    public float rotateSpeed;
    public bool isAttacking;
    public float baseOffset;
    public float t;
    public float range;
    public Quaternion attackOffset;
    public Vector3 attackStartRotation;
    public Vector3 attackEndRotation;

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
        range = 2;
        baseOffset = .5f;
        offsetDistance = baseOffset;
        dpt = 1;
        tickRate = 10;
        tickTimer = 0;
        attackSpeed = .5f;
        t = 0;
        attackOffset = Quaternion.Euler(new Vector3(0, 0, 15));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        tickTimer = (tickTimer - 1);
        if (tickTimer < 0) tickTimer = tickRate;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (player == null) player = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>();
        float angle = Vector2.Angle(Vector2.right, player.mousePos);
        if (player.mousePos.y < 0) angle = angle * -1;
        angle += 180;

        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * swingSpeed));

        if (!isAttacking && t > 0) t -= (Time.fixedDeltaTime / attackSpeed) * 8;
        if (t < 0) t = 0;
        offsetDistance = Mathf.Lerp(baseOffset, range, t);
        Vector3 offsetVec = new Vector3(player.mousePos.x, player.mousePos.y, 0).normalized * offsetDistance;

        rb.MovePosition((player.gameObject.transform.position + offsetVec) * Time.fixedDeltaTime * followSpeed * 10);
    }

    public override void Attack(Vector2 mousePos)
    {
        isAttacking = true;
        t += (Time.fixedDeltaTime / attackSpeed) * 4;
        if (t > 1) t = 1;
        attackStartRotation = attackOffset * new Vector3(player.mousePos.x, player.mousePos.y, 0).normalized;


        StartCoroutine(attackAnim());
    }



    IEnumerator attackAnim()
    {
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }
}
