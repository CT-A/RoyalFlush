using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAnimationHandler : MonoBehaviour
{
    public GameObject explosionSprite;
    public void explosion(Vector3 pos)
    {
        Instantiate(explosionSprite, pos, Quaternion.identity);
    }
}
