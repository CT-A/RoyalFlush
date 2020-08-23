﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Drop
{
    public string name;
    public override void Pickup(PlayerControlls pc)
    {
        pc.Pickup(name);
        base.Pickup(pc);
    }
}
