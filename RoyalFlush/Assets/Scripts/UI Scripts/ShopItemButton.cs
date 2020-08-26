using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    public Image img;
    public string name;
    public Text price;
    public Text label;
    public DropHandler dh;
    // Start is called before the first frame update
    void Start()
    {
        dh = GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh;
    }
    public void Stock(string itemStocking)
    {
        name = itemStocking;
        price.text = dh.possibleItemsDict[name].GetComponent<Item>().price.ToString();
        label.text = name;
    }
    public void UpdateSprite()
    {
        img.sprite = dh.possibleItemsDict[name].GetComponent<SpriteRenderer>().sprite;
    }
    public void Buy()
    {
        GameManager gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        if(gm.Buy(this)) gameObject.SetActive(false);
    }
}
