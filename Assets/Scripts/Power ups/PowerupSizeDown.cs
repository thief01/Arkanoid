using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSizeDown : Powerup
{
    public override void Execute(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlatformController>().SizeDown();
    }
}
