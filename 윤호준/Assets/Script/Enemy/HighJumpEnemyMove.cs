using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpEnemyMove : EnemyMove
{
    public override void tread(PlayerMove p){
       p.jump();
       p.jumpPower = 40f;
   }
}

