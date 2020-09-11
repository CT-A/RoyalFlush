using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    public TMP_Text g;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        g = gameObject.GetComponent<TMP_Text>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        g.text = "$" + gm.GetGold();
    }
}
