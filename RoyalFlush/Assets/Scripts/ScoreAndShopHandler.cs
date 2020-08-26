using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAndShopHandler : MonoBehaviour
{
    public ShopItemButton[] toStock;
    public DropHandler dh;

    public void Stock()
    {
        dh = GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh;
        toStock = new ShopItemButton[2];
        GameObject[] ts = GameObject.FindGameObjectsWithTag("ShopItem");
        int i = 0;
        foreach(GameObject t in ts)
        {
            toStock[i] = t.GetComponent<ShopItemButton>();
            i++;
        }
        foreach(ShopItemButton item in toStock)
        {
            int randomIdx = (int)Mathf.Floor(UnityEngine.Random.Range(0f, dh.dropsLeft.Count));
            //Debug.Log(randomIdx);
            //Debug.Log(dh.dropsLeft.Count);
            string itemToStock = dh.dropsLeft[randomIdx];
            item.Stock(itemToStock);
            item.UpdateSprite();
        }
    }
    public int Score(string[] hand)
    {
        if (hand[0] == null) return 0;
        if (hand[0].Length == 0) return 0;
        int numA = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        int num8 = 0;
        int num9 = 0;
        int num10 = 0;
        int numJ = 0;
        int numQ = 0;
        int numK = 0;
        bool flush = true;
        bool straight = true;
        string suit = hand[0].Substring(hand[0].Length - 1, 1);
        int[] sorted = new int[5];
        int temp = -1;
        int idx = 0;
        foreach (string c in hand)
        {
            string card = c;
            if (card == null) card = "";
            if (card.EndsWith(suit)&&flush)
            {
                flush = true;
                suit = card.Substring(card.Length - 1, 1);
            }
            else flush = false;

            if (card.StartsWith("card_A"))
            {
                numA += 1;
                sorted[idx] = 0;
            }
            if (card.StartsWith("card_2"))
            {
                num2 += 1;
                sorted[idx] = 1;
            }
            if (card.StartsWith("card_3"))
            {
                num3 += 1;
                sorted[idx] = 2;
            }
            if (card.StartsWith("card_4"))
            {
                num4 += 1;
                sorted[idx] = 3;
            }
            if (card.StartsWith("card_5"))
            {
                num5 += 1;
                sorted[idx] = 4;
            }
            if (card.StartsWith("card_6"))
            {
                num6 += 1;
                sorted[idx] = 5;
            }
            if (card.StartsWith("card_7"))
            {
                num7 += 1;
                sorted[idx] = 6;
            }
            if (card.StartsWith("card_8"))
            {
                num8 += 1;
                sorted[idx] = 7;
            }
            if (card.StartsWith("card_9"))
            {
                num9 += 1;
                sorted[idx] = 8;
            }
            if (card.StartsWith("card_0"))
            {
                num10 += 1;
                sorted[idx] = 9;
            }
            if (card.StartsWith("card_J"))
            {
                numJ += 1;
                sorted[idx] = 10;
            }
            if (card.StartsWith("card_Q"))
            {
                numQ += 1;
                sorted[idx] = 11;
            }
            if (card.StartsWith("card_K"))
            {
                numK += 1;
                sorted[idx] = 12;
            }
            idx += 1;
        }
        Array.Sort(sorted);
        temp = sorted[0] - 1;
        foreach(int c in sorted)
        {
            if ((c == (temp + 1)) && straight)
            {
                temp = c;
            }
            else straight = false;
        }

        int[] counts = new int[] { numA, num2, num3, num4, num5, num6, num7, num8, num9, num10, numJ, numQ, numK };
        bool has4 = false;
        bool has3 = false;
        bool has2 = false;
        bool has22 = false;
        foreach (int n in counts) // go over every number in the list
        {
            if (n == 2) // check if it matches
            {
                if (has2) has22 = true;
                has2 = true;
            }
            if (n == 3) // check if it matches
            {
                has3 = true;
            }
            if (n == 4) // check if it matches
            {
                has4 = true;
            }
        }
        if (flush && straight)
        {
            if (sorted[0] == 9)
            {
                return 20;
            }
            else return 8;
        }
        else if (has4)
        {
            return 7;
        }
        else if (has3 & has2)
        {
            return 6;
        }
        else if (flush) return 5;
        else if (straight) return 4;
        else if (has3) return 3;
        else if (has22) return 2;
        else if (has2) return 1;
        else return 0;
    }
}
