using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text text;

    void Update()
    {
        text.text = "X" + (GameObject.FindWithTag("GameController").GetComponent<GameManager>().sash.Score(GameObject.FindWithTag("Player").GetComponent<PlayerControlls>().i.hand) + 1);
    }
}
