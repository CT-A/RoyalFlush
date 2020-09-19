using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon wep;
    public bool isBullet;

    public float range;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(Weapon w, bool bullet, float r)
    {
        wep = w;
        isBullet = bullet;
        range = r;
        Destroy(gameObject, r);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Enemy")
        {
            EnemyController es = other.gameObject.GetComponent<EnemyController>();
            es.Hurt(wep.dpt);
            if (isBullet) Destroy(gameObject);
        }
        //else if (other.tag == "Collidable") Destroy(gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("triggerStay");
        if (other.tag == "Enemy")
        {
            EnemyController es = other.gameObject.GetComponent<EnemyController>();
            if (wep.tickTimer == 0) es.hp -= wep.dpt;
        }
    }

}
