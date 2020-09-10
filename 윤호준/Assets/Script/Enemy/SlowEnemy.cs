using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : EnemyMove
{
    private Coroutine co;
    public float buffTime = 3f;

    protected override void Awake()
    {
        base.Awake();
        sustainmentTime = buffTime;
        buffIcon = spriteRenderer.sprite;
    }

    public override void tread(PlayerMove p)
    {
        base.tread(p);
        buff(true);

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
