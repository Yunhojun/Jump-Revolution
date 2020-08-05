using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpEnemyMove : EnemyMove
{
    public override void tread(PlayerMove p){
       p.Jump();
       p.jumpCount = 1;
       Destroy();
   }
}
