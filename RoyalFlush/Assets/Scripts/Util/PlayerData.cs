using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int maxHealth;
    public int gold;
    public float[] position;
    public List<string> dropsLeft;
    public float minCardsLeft;
    public int cardsDropped;
    public List<string> cardsLeft;

    public PlayerData(PlayerControlls player) {
        //IF YOU UPDATE THIS, UPDATE THE LOAD FROM SAVE FUNCTION IN PLAYER
        dropsLeft = GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.dropsLeft;
        cardsLeft = GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.cardsLeft;
        minCardsLeft = GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.minCardsLeft;
        cardsDropped = GameObject.FindWithTag("GameController").GetComponent<GameManager>().dh.cardsDropped;
        maxHealth = player.maxHP;
        health = player.hp;
        gold = player.gold;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
