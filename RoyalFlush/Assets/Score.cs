using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text text;

    void Update()
    {
        text.text = "Multiplier:" + (GameObject.FindWithTag("GameController").GetComponent<GameManager>().sash.Score(GameObject.FindWithTag("Player").GetComponent<PlayerControlls>().i.hand) + 1);
    }
}
