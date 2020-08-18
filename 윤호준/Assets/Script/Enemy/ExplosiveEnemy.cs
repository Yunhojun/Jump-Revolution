using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ExplosiveEnemy : EnemyMove
{
    public GameObject explosion;
    [SerializeField]
    private float force = 1500;
    
    protected override void move()
    {
        base.move();
    }


    public override void tread(PlayerMove p)
    {
        Rigidbody2D playerRigid = p.rigid;
        Vector2 dir = playerRigid.position - rigid.position;
        playerRigid.AddForce(dir * force);
        p.Stun(1.5f);
        //폭발 이펙트
        SoundScript.Inst.explosionPlayer();
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy();
    }
}
