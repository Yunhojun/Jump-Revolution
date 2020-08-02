using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : EnemyMove
{
    [SerializeField]
    private int slowRate = 50;
    PlayerMove p = null;
    [SerializeField]
    float slowTime = 3f;

    public override void tread(PlayerMove p)
    {
        base.tread(p);
        this.p = p;
        float f = 5 * (1 - ((float)slowRate / 100));
        p.moveSpeed = f;
        Invoke("Recover", slowTime);
        Destroy();
    }

    private void Recover()
    {
        p.moveSpeed = 5;
    }
}
