using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public bool empty;
    public string name;
    public int handIdx;
    public int cardsIdx;
    public Image img;
    public DropHandler dh;
    public Sprite emptySprite;

    void Start()
    {
        dh = GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh;
    }

    public void SetAll(CardInfo info)
    {
        empty = info.empty;
        name = info.name;
        handIdx = info.handIdx;
        cardsIdx = info.cardsIdx;
    }

    public void UpdateSprite()
    {
        if (!empty) img.sprite = dh.possibleCardsDict[name].GetComponent<SpriteRenderer>().sprite;
        else img.sprite = emptySprite;
    }

    public void SetTo(CardInfo newInfo)
    {
        empty = newInfo.empty;
        name = newInfo.name;
        //handIdx = newInfo.handIdx;
        //cardsIdx = newInfo.cardsIdx;
    }
}
