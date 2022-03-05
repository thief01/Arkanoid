using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSizeUp : Powerup
{
    public override void Execute(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlatformController>().SizeUp();
    }
}
