using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthHud : MonoBehaviour
{
    public Image heart;
    public Sprite full;
    public Sprite half;
    public Sprite empty;
    public List<Image> hearts;
    public PlayerControlls p;
    void Start()
    {

        StartCoroutine(LateStart(.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        InstantiateHearts();
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateHearts()
    {
        foreach (Image heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();
        int offset = 50;
        for (int i = 0; i < p.maxHP; i+=2)
        {
            hearts.Add(Instantiate(heart, new Vector3(transform.position.x + i * offset, transform.position.y, transform.position.z), Quaternion.identity,gameObject.transform));
        }
    }
    public void UpdateSprite()
    {
        if (p.maxHP / 2 > hearts.Count)
            InstantiateHearts();
        //just in case player is dead
        if (p.hp >= 0)
        {
            //if hp is even, fill all hearts up to hp, then empty all other hearts
            if (p.hp % 2 == 0)
            {
                for (int i = 0; i < p.hp / 2; i += 1)
                {
                    hearts[i].sprite = full;
                }
                for (int r = (int)p.hp / 2; r < p.maxHP / 2; r += 1)
                {
                    hearts[r].sprite = empty;
                }
            }
            //if hp is odd, fill up to (hp-1)/2 then make (hp-1)/2 half, then empty (hp+1)/2 to max/2
            else
            {
                for (int i = 0; i < (p.hp - 1) / 2; i += 1)
                {
                    hearts[i].sprite = full;
                }
                hearts[(int)(p.hp - 1) / 2].sprite = half;
                for (int r = (int)(p.hp + 1) / 2; r < p.maxHP / 2; r += 1)
                {
                    hearts[r].sprite = empty;
                }
            }
        }
    }

}
