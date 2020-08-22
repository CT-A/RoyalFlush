using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandler : MonoBehaviour
{
    public int maxItems;
    public float itemChance;
    public List<GameObject> possibleDrops;
    public List<GameObject> dropsLeft;
    public float goldChance;

    public void drop(Vector3 position)
    {
        if (Random.value <= itemChance)
        {
            int randomIdx = (int) Mathf.Floor(Random.Range(1f, dropsLeft.Count) - 1);
            Debug.Log(randomIdx);
            GameObject itemToDrop = dropsLeft[randomIdx];
            //Instantiate(itemToDrop, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
