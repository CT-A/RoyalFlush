using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 towardsPlayer = (player.transform.position - transform.position).normalized;
        rb.velocity = (towardsPlayer * 7);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.tag == "Player")
        {
            PlayerControlls p = other.gameObject.GetComponent<PlayerControlls>();
            p.GainXP();
            Destroy(gameObject);
        }
    }
}
