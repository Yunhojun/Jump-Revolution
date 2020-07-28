using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemyMove : EnemyMove
{
    public override void tread(PlayerMove p){
       p.jump();
       p.SetDashCount(1);
   }
}
