﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandler : MonoBehaviour
{
    public int maxItems;
    public float itemChance;
    public float cardChance;
    //possibleDrops is cards
    public List<GameObject> possibleDrops;
    //possibleItemDrops is non-card items
    public List<GameObject> possibleItemDrops;
    public Dictionary<string, GameObject> possibleCardsDict;
    public Dictionary<string, GameObject> possibleItemsDict;
    public List<string> dropsLeft;
    public List<string> cardsLeft;
    public float goldChance;
    public float minCardsLeft;
    public int cardsDropped;
    public int maxCards;

    public float curCardChance;

    public int goldDropped;
    public int maxGold;
    public float minGoldPerPile;
    public float maxGoldPerPile;
    public GameObject goldPile;
    public GameManager gm;

    void Start()
    {
        minGoldPerPile = 10;
        maxGoldPerPile = 100;
        goldDropped = 0;
        maxGold = 500;
        curCardChance = cardChance;
        cardsDropped = 0;
        maxCards = 15;
        minCardsLeft = 5;
        possibleCardsDict = new Dictionary<string, GameObject>();
        foreach (GameObject card in possibleDrops)
        {
            string n = card.GetComponent<Item>().name;
            possibleCardsDict.Add(n,card);
            cardsLeft.Add(n);
        }
        possibleItemsDict = new Dictionary<string, GameObject>();
        foreach (GameObject item in possibleItemDrops)
        {
            string n = item.GetComponent<Item>().name;
            possibleItemsDict.Add(n, item);
            dropsLeft.Add(n);
        }
    }
    public void drop(Vector3 position)
    {
        if ((gm.numEnemies == 0)||(minCardsLeft <= 0))
            curCardChance = cardChance;
        else curCardChance = Mathf.Max(cardChance, (minCardsLeft/gm.numEnemies));
        //guarenteed card chance = (float) minimumCardsToDropLeft / remainingEnemies
        //drop a card based on max of guarenteed and fixed card chance
        if ((Random.value <= curCardChance) && (cardsDropped < maxCards))
        {
            int randomIdx = (int)Mathf.Floor(Random.Range(1f, cardsLeft.Count) - 1);
            GameObject cardToDrop = possibleCardsDict[cardsLeft[randomIdx]];
            Instantiate(cardToDrop, position, Quaternion.identity);
            cardsDropped += 1;
            minCardsLeft -= 1;
            cardsLeft.Remove(cardsLeft[randomIdx]);
        }
        //not dropping a card, check if dropping an item
        else if (Random.value <= itemChance)
        {
            int randomIdx = (int)Mathf.Floor(Random.Range(1f, dropsLeft.Count) - 1);
            GameObject itemToDrop = possibleItemsDict[dropsLeft[randomIdx]];
            Instantiate(itemToDrop, position, Quaternion.identity);
        }
        //didnt drop a card or item, check if dropping gold
        else if ((Random.value <= goldChance)&&(goldDropped < maxGold))
        {
            int amtGold = (int)Mathf.Floor(Random.Range(10f, 100f));
            goldDropped += amtGold;
            GameObject goldToDrop = goldPile;
            goldToDrop.GetComponent<GoldPile>().gold = amtGold;
            Instantiate(goldToDrop, position, Quaternion.identity);
        }
    }
}