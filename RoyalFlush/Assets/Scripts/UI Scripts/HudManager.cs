using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    public WeaponHud wh;
    public HealthHud hh;

    public void UpdateWeaponHUD()
    {
        wh.UpdateSprite();
    }
    public void UpdateHpHud()
    {
        hh.UpdateSprite();
    }

    public void UpdateXP(float c, float t)
    {
        wh.UpdateXP(c,t);
    }
}
