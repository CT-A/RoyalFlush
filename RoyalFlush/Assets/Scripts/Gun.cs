using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
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
    public SpriteRenderer gunSprite;

    public float attackCooldown;

    // Start is called before the first frame update
    public override void InstantiateWeapon(PlayerControlls pc)
    {
        Gun wep = Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Gun>();
        wep.rb = wep.gameObject.GetComponent<Rigidbody2D>();
        wep.player = pc;
        wep.followSpeed = wep.player.moveSpeed;
        wep.rotateSpeed = 5f;
        wep.player.weapon = this;
        wep.isAttacking = false;
        wep.baseOffset = .2f;
        wep.swingSpeed = 20f;
        wep.offsetDistance = wep.baseOffset;
        wep.dpt = 1;
        wep.tickRate = 10;
        wep.tickTimer = 0;
        wep.t = 0;
        wep.level = 0;
        wep.range = 0.2f;
        wep.gunSprite = wep.GetComponentInChildren<SpriteRenderer>();
        wep.sprites[level] = sprites[level];
        wep.damages[level] = damages[level];
        wep.attackSpeeds[level] = attackSpeeds[level];
        wep.gunSprite.sprite = wep.sprites[level];
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
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (player == null) player = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>();
        if (player.mousePos.x < 0) gunSprite.flipY = false;
        else gunSprite.flipY = true;
        tickTimer = (tickTimer - 1);
        if (tickTimer < 0) tickTimer = tickRate;
        float angle = Vector2.Angle(Vector2.right, player.mousePos);
        if (player.mousePos.y < 0) angle = angle * -1;
        angle += 180;

        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * swingSpeed));

        //if (!isAttacking && t > 0) t -= (Time.fixedDeltaTime / attackSpeed) * 8;
        //if (t < 0) t = 0;
        offsetDistance = .5f;//Mathf.Lerp(baseOffset, range, t);
        Vector3 offsetVec = new Vector3(player.mousePos.x, player.mousePos.y, 0).normalized * offsetDistance;

        rb.MovePosition((player.gameObject.transform.position + offsetVec) * Time.fixedDeltaTime * followSpeed * 10);
    }

    public override void Attack(Vector2 mousePos)
    {

        t += (Time.fixedDeltaTime / attackSpeed) * 4;
        if (t > 1) t = 1;

        if (!isAttacking && attackCooldown <= 0)
        {
            Vector2 offsetVec = new Vector2(player.mousePos.x, player.mousePos.y).normalized * (offsetDistance + .5f);
            GameObject arro = Instantiate(arrow, rb.position + offsetVec, gameObject.transform.rotation);
            arrow.transform.localScale = gameObject.transform.localScale;
            //arro.GetComponent<Rigidbody2D>().velocity = new Vector2(player.mousePos.x, player.mousePos.y).normalized * 10;
            arro.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.mousePos.x, player.mousePos.y).normalized * 1000);
            //arro.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10);
            arro.GetComponent<Projectile>().initialize(this, false,range);
            attackCooldown = attackSpeed;
            isAttacking = true;
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        gunSprite.sprite = sprites[level];
        dpt = damages[level];
        attackSpeed = attackSpeeds[level];
    }
}
