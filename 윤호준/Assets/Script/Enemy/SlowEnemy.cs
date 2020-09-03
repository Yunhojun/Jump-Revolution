using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : EnemyMove
{
    private Coroutine co;
    public float sustainmentTime = 3f;
    public override void tread(PlayerMove p)
    {
        base.tread(p);
        p.moveSpeed = 2.5f;
        co = p.GetCoroutine();
        if (p.isNormalSpeed)
        {
            p.isNormalSpeed = false;
            p.SetCoroutine(sustainmentTime);
            co = p.GetCoroutine();
        }
        else
        {
            co = p.GetCoroutine();
            StopCoroutine(co);
            p.SetCoroutine(sustainmentTime);
        }
    }
}
