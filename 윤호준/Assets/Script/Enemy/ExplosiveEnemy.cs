using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : EnemyMove
{
    [SerializeField]
    private float force = 1500;

    protected override void move()
    {
        base.move();
    }
}
/*
    public override void tread(PlayerMove p)
    {
        Rigidbody2D playerRigid = p.GetRig();
        Vector2 dir = playerRigid.position - rigid.position;
        playerRigid.AddForce(dir * force);
        p.stun();
        //폭발 이펙트
        Destroy();
    }
}
*/