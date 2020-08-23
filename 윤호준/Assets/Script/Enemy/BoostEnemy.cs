using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEnemy : EnemyMove
{    
    private Coroutine co;
    public override void tread(PlayerMove p)
    {
        p.Jump();
        p.moveSpeed = 10f;
        Destroy();
        co = p.GetCoroutine();
        if (p.isNormalSpeed)
        {
            p.isNormalSpeed = false;
            p.SetCoroutine(0);
            co = p.GetCoroutine();
        }
        else
        {
            co = p.GetCoroutine();
            StopCoroutine(co);
            p.SetCoroutine(0);
        }
    }
}
