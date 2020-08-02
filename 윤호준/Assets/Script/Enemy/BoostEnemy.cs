using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEnemy : EnemyMove
{
    [SerializeField]
    private int boostRate = 50;
    PlayerMove p = null;
    [SerializeField]
    float boostTime = 3f;

    public override void tread(PlayerMove p)
    {
        base.tread(p);
        this.p = p;
        float f = 5 * (1 + ((float)boostRate / 100));
        p.moveSpeed = f;
        Invoke("Recover", boostTime);

    }

    private void Recover()
    {
        p.moveSpeed = 5;
    }
}
