using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bow : Weapon
{
    public PlayerControlls player;

    private Rigidbody2D rb;

    public float offsetDistance;
    public float swingSpeed;
    public float followSpeed;
    public float rotateSpeed;
    public bool isAttacking;
    public float baseOffset;
    public float t;
    public Vector3 attackOffset;
    public GameObject arrow;

    public float attackCooldown;

    // Start is called before the first frame update
    public override void InstantiateWeapon(PlayerControlls pc)
    {
        Bow wep = Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Bow>();
        wep.rb = wep.gameObject.GetComponent<Rigidbody2D>();
        wep.player = pc;
        wep.followSpeed = wep.player.moveSpeed;
        wep.rotateSpeed = 5f;
        wep.player.weapon = this;
        wep.isAttacking = false;
        wep.baseOffset = .5f;
        wep.swingSpeed = 20f;
        wep.offsetDistance = wep.baseOffset;
        wep.dpt = 1;
        wep.tickRate = 10;
        wep.tickTimer = 0;
        wep.attackSpeed = .5f;
        wep.t = 0;
        wep.level = 0;
        wep.range = 0.5f;
        wep.sprites[level] = sprites[level];
        wep.damages[level] = damages[level];
        wep.attackSpeeds[level] = attackSpeeds[level];
        wep.GetComponentInChildren<SpriteRenderer>().sprite = wep.sprites[level];
        wep.dpt = wep.damages[level];
        wep.attackSpeed = wep.attackSpeeds[level];
        wep.attackCooldown = wep.attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0 && isAttacking) isAttacking = false;
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

        //if (!isAttacking && t > 0) t -= (Time.fixedDeltaTime / attackSpeed) * 8;
        //if (t < 0) t = 0;
        offsetDistance = 1;//Mathf.Lerp(baseOffset, range, t);
        Vector3 offsetVec = new Vector3(player.mousePos.x, player.mousePos.y, 0).normalized * offsetDistance;

        rb.MovePosition((player.gameObject.transform.position + offsetVec) * Time.fixedDeltaTime * followSpeed * 10);
    }

    public override void Attack(Vector2 mousePos)
    {
        
        //t += (Time.fixedDeltaTime / attackSpeed) * 4;
        //if (t > 1) t = 1;

        if (!isAttacking && attackCooldown <= 0)
        {
            GameObject arro = Instantiate(arrow, rb.position, gameObject.transform.rotation);
            arro.transform.localScale = gameObject.transform.localScale;
            arro.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.mousePos.x, player.mousePos.y).normalized * 1000);
            arro.GetComponent<Projectile>().initialize(dpt,false,range);
            attackCooldown = attackSpeed;
            isAttacking = true;
        }
    }


    public override void LevelUp()
    {
        base.LevelUp();
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[level];
        dpt = damages[level];
        attackSpeed = attackSpeeds[level];
    }
}
