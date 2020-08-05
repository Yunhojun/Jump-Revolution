using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippleJumpEnemyMove : EnemyMove
{
    public override void tread(PlayerMove p){
       p.Jump();
       p.jumpCount = 2;
       Destroy();
   }
}
