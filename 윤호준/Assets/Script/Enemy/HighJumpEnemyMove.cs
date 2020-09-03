using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpEnemyMove : EnemyMove
{
    private static Coroutine co;
    public float sustainmentTime = 3f;
    public override void tread(PlayerMove p)
    {
        p.jumpPower = 41f;
        base.tread(p);

        if (p.highJumpOn == false)
        {
            p.highJumpOn = true;
            co = StartCoroutine(SustainmentHighJump(p));
        }
        else
        {
            StopCoroutine(co);
            co = StartCoroutine(SustainmentHighJump(p));
        }

    }

    IEnumerator SustainmentHighJump(PlayerMove p2)
    {
        yield return new WaitForSeconds(sustainmentTime);
        p2.jumpPower = 21f;
        p2.highJumpOn = false;
    }
}

