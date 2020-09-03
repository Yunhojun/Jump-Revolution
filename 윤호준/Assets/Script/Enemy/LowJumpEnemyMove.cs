using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowJumpEnemyMove : EnemyMove
{
    private static Coroutine co;
    public float sustainmentTime = 3f;
    public override void tread(PlayerMove p)
    {
        base.tread(p);
        p.jumpPower = 10f;

        if (p.lowJumpOn == false)
        {
            p.lowJumpOn = true;
            co = StartCoroutine(SustainmentLowJump(p));
        }
        else
        {
            StopCoroutine(co);
            co = StartCoroutine(SustainmentLowJump(p));
        }

    }

    IEnumerator SustainmentLowJump(PlayerMove p2)
    {
        yield return new WaitForSeconds(sustainmentTime);
        p2.jumpPower = 21f;
        p2.lowJumpOn = false;
    }


}
