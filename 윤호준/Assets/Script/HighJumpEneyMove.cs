using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpEneyMove : EnemyMove
{
    public override void tread(PlayerMove p){
       p.jump();
       p.jumpPower = 40f;
   }
}

