using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float dpt;

    public int tickRate;

    public int tickTimer;

    public bool isBullet;

    public float range;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialize(float dmg, bool bullet, float r)
    {
        dpt = dmg;
        isBullet = bullet;
        range = r;
        Destroy(gameObject, r);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Enemy")
        {
            EnemyController es = other.gameObject.GetComponent<EnemyController>();
            es.hp -= dpt;
            if (isBullet) Destroy(gameObject);
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
