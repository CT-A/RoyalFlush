using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items;
    public string[] cards;
    public string[] hand;
    public SwapHandler sh;

    void Start()
    {
        hand = new string[5];
        cards = new string[10];
    }

    public void Pickup(string item)
    {
        //card names like "card_AS" for ace of spades
        // suits are styled as C,H,S,D for clubs, hearts, spades, diamonds
        // numbers as A234567890 (10 is 0) JQK 
        if (item.StartsWith("card_"))
        {
            int hIdx = -1;
            int cIdx = -1;
            bool putInHand = false;
            int i = 0;
            while (i < hand.Length)
            {
                if ((hand[i] == null) || (hand[i] == ""))
                {
                    hand[i] = item;
                    hIdx = i;
                    putInHand = true;
                    i = hand.Length;
                }
                i++;
            }
            if (!putInHand)
            {
                //cards.Add(item);
                int j = 0;
                while (j < cards.Length)
                {
                    if ((cards[j] == null) || (cards[j] == ""))
                    {
                        cards[j] = item;
                        cIdx = j;
                        j = cards.Length;
                    }
                    j++;
                }
            }
            GameObject tempObject = new GameObject();
            tempObject.AddComponent<CardInfo>();
            CardInfo temp = tempObject.GetComponent<CardInfo>();
            temp.empty = false;
            temp.handIdx = hIdx;
            temp.cardsIdx = cIdx;
            temp.name = item;
            sh.AddCard(temp);
        }
        else items.Add(item);
    }
}
