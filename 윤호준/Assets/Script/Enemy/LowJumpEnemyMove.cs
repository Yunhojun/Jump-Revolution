using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowJumpEnemyMove : EnemyMove
{
    public override void tread(PlayerMove p){
       p.Jump();
       p.jumpPower = 10f;
       Destroy();
   }
}
