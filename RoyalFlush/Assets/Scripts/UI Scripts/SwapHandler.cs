using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapHandler : MonoBehaviour
{
    public GameObject heldCard;
    public Inventory i;
    public GameObject[] cards;
    public GameObject[] hand;
    public void SwapCards(GameObject otherCard)
    {
        GameObject tempObject = new GameObject();
        tempObject.AddComponent<CardInfo>();
        CardInfo temp = tempObject.GetComponent<CardInfo>();
        temp.SetAll(otherCard.GetComponent<CardInfo>());
        otherCard.GetComponent<CardInfo>().SetTo(heldCard.GetComponent<CardInfo>());
        otherCard.GetComponent<CardInfo>().UpdateSprite();
        heldCard.GetComponent<CardInfo>().SetTo(temp);
        heldCard.GetComponent<CardInfo>().UpdateSprite();

        if (heldCard.GetComponent<CardInfo>().handIdx >= 0)
        {
            i.hand[heldCard.GetComponent<CardInfo>().handIdx] = heldCard.GetComponent<CardInfo>().name;
        }
        else
        {
            i.cards[heldCard.GetComponent<CardInfo>().cardsIdx] = heldCard.GetComponent<CardInfo>().name;
        }
        if (otherCard.GetComponent<CardInfo>().handIdx >= 0)
        {
            i.hand[otherCard.GetComponent<CardInfo>().handIdx] = otherCard.GetComponent<CardInfo>().name;
        }
        else
        {
            i.cards[otherCard.GetComponent<CardInfo>().cardsIdx] = otherCard.GetComponent<CardInfo>().name;
        }
        //if (temp.handIdx >= 0)
        //{
        //    i.hand[temp.handIdx] = heldCard.GetComponent<CardInfo>().name;
        //}
        //else
        //{
        //    i.cards[temp.cardsIdx] = heldCard.GetComponent<CardInfo>().name;
        //}
        //if ((heldCard.GetComponent<CardInfo>().handIdx >= 0))
        //{
        //    if (!otherCard.GetComponent<CardInfo>().empty)
        //        i.hand[heldCard.GetComponent<CardInfo>().handIdx] = temp.name;
        //    else
        //        i.hand[heldCard.GetComponent<CardInfo>().handIdx] = null;
        //}
        //else
        //{
        //    if (!otherCard.GetComponent<CardInfo>().empty)
        //        i.cards[heldCard.GetComponent<CardInfo>().cardsIdx] = temp.name;
        //    else
        //        i.cards[heldCard.GetComponent<CardInfo>().cardsIdx] = null;
        //}


    }
    public void AddCard(CardInfo newCard)
    {
        if (newCard.handIdx >= 0)
        {
            hand[newCard.handIdx].GetComponent<CardInfo>().SetTo(newCard);
            hand[newCard.handIdx].GetComponent<CardInfo>().UpdateSprite();
        }
        else
        {
            cards[newCard.cardsIdx].GetComponent<CardInfo>().SetTo(newCard);
            cards[newCard.cardsIdx].GetComponent<CardInfo>().UpdateSprite();
        }
    }
}
