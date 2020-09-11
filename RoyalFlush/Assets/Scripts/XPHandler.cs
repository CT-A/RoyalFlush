using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPHandler : MonoBehaviour
{
    public GameObject xp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn(Vector3 pos, int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            //Debug.Log("spawned xp");
            Instantiate(xp, pos, Quaternion.identity);
        }
    }
}
