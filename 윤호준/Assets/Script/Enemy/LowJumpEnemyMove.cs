using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowJumpEnemyMove : EnemyMove
{
    public float second = 5f;
    float jumpPower = 10f;
    public override void tread(PlayerMove p){
       p.Jump();
       p.jumpPower = jumpPower;
       Destroy();
    }

    

}
