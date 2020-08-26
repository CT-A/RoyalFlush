using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //damage per tick
    public float dpt;
    public float attackSpeed;
    public float range;

    //Number of fixed updates before causing dmg (there are 50 fixedUpdates per second, so a tickRate of 1 is 50 damage procs a second)
    public int tickRate;

    public int tickTimer;

    public int level;
    public int size;
    public Sprite[] sprites;
    public int[] damages;
    public float[] attackSpeeds;


    public virtual void InstantiateWeapon(PlayerControlls pc)
    {
    }

    public virtual void Attack(Vector2 mousePos)
    {

    }

    public virtual void LevelUp()
    {
        if (level < 4)
        {
            size = level;
            range += .5f;
        }
        gameObject.transform.localScale = new Vector3(1 + size / 10f, 1 + size / 10f, 1 + size / 10f);
        level += 1 ;
        if (level >= 4)
        {
            level = 4;
            dpt += 1;
            size += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.tag == "Enemy")
        {
            EnemyController es = other.gameObject.GetComponent<EnemyController>();
            es.hp -= dpt;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("triggerStay");
        if (other.tag == "Enemy")
        {
            EnemyController es = other.gameObject.GetComponent<EnemyController>();
            if (tickTimer == 0) es.hp -= dpt;
        }
    }
}
