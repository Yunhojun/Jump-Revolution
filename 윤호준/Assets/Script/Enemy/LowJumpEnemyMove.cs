using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowJumpEnemyMove : EnemyMove
{
    private static Coroutine co;
    public override void tread(PlayerMove p){
        p.Jump();
        p.jumpPower = 10f;
        Destroy();
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
        yield return new WaitForSeconds(5f);
        p2.jumpPower = 21f;
        p2.lowJumpOn = false;
    }


}
