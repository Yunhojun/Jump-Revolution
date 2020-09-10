using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEnemy : EnemyMove
{    
    private Coroutine co;
    public float BoostTime = 3f;

    protected override void Awake()
    {
        base.Awake();
        buffIcon = spriteRenderer.sprite;
        sustainmentTime = BoostTime;
    }

    public override void tread(PlayerMove p)
    {
        base.tread(p);
        buff(true);

        p.moveSpeed = 10f;
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
