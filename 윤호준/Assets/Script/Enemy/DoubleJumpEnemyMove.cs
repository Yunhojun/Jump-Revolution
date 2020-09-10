using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpEnemyMove : EnemyMove
{

    public override void tread(PlayerMove p)
    {
        base.tread(p);

        p.jumpCount = 1;
    }
}
