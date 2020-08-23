using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPile : Drop
{
    public int gold;
    public override void Pickup(PlayerControlls pc)
    {
        if (gold != null)
        {
            pc.gold += gold;
        }
        base.Pickup(pc);
    }
}
