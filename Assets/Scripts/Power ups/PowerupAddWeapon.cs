using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAddWeapon : Powerup
{
    public override void Execute(Collision2D collision)
    {
        collision.gameObject.GetComponent<WeaponController>().AddWeapon();
    }
}
