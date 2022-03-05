using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCloneBall : Powerup
{
    public override void Execute(Collision2D collision)
    {
        GameState.instace.CloneBalls();
    }
}
