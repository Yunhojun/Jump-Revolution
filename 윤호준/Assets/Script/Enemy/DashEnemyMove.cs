using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemyMove : EnemyMove
{
    public override void tread(PlayerMove p)
    {
        base.tread(p);

        p.dashCount = 1;
    }
}
