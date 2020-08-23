using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("pickup triggered");
        if (other.tag == "Player")
        {
            //Debug.Log("by player");
            Pickup(other.gameObject.GetComponent<PlayerControlls>());
        }
    }

    public virtual void Pickup(PlayerControlls pc)
    {
        Destroy(gameObject);
    }
}
