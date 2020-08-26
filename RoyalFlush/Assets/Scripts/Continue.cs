using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    public void nextLevel()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().NextLevel();
    }
}
