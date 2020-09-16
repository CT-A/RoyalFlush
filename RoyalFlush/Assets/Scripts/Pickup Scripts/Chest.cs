using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject item;
    public Sprite open;
    public SpriteRenderer sr;

    private void Start()
    {
    }

    void Open()
    {
        sr.sprite = open;
        StartCoroutine(Poof(.2f));
    }

    IEnumerator Poof(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Open();
        }
    }
}
