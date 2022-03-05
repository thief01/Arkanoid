using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupAddBall : Powerup
{
    public override void Execute(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlatformController>().AddBall();
    }
}
