using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHud : MonoBehaviour
{
    public Image img;
    public Image xpImg;
    public Weapon w;
    public TMP_Text lvlText;
    
    void Start()
    {
        StartCoroutine(LateStart(.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        w = GameObject.FindWithTag("Weapon").GetComponent<Weapon>();
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite()
    {
        img.sprite = w.sprites[w.level];
        //Debug.Log("sprite updated to level: " + w.level);
        int displayLvl = w.level;
        if (w.size > displayLvl)
            displayLvl = w.size;
        lvlText.text = "LVL " + displayLvl;
    }

    public void UpdateXP(float c, float t)
    {
        //Debug.Log(amt);
        xpImg.gameObject.transform.localScale = new Vector3(c/t, 1f, 1f);
    }
}
