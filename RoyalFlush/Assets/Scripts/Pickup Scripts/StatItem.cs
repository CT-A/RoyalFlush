using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatItem : Item
{
    public int hp;
    public int ms;
    public int dmg;
    public int range;
    public int aSpeed;

    public override void Pickup(PlayerControlls pc)
    {
        pc.hp += hp;
        pc.maxHP += hp;
        pc.moveSpeed += ms;
        pc.weapon.dpt += dmg;
        pc.weapon.attackSpeed += aSpeed;
        pc.weapon.range += range;
        pc.Pickup(name);
    }
}
