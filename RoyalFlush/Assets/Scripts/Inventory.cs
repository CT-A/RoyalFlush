using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items;
    public List<string> cards;
    public string[] hand;

    void Start()
    {
        hand = new string[5];
    }
    public void Pickup(string item)
    {
        //card names like "card_AS" for ace of spades
        // suits are styled as C,H,S,D for clubs, hearts, spades, diamonds
        // numbers as A234567890 (10 is 0) JQK 
        if (item.StartsWith("card_"))
        {
            bool putInHand = false;
            int i = 0;
            while (i < hand.Length)
            {
                if (hand[i] == null)
                {
                    hand[i] = item;
                    putInHand = true;
                    i = hand.Length;
                }
                i++;
            }
            if (!putInHand) cards.Add(item);
        }
        else items.Add(item);
    }
}
