using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FlyingEnemyMove : EnemyMove
{
    protected int nextMoveX = 1;
    protected int nextMoveY = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ScaleX")
        {
            nextMoveX *= -1;
            spriteRenderer.flipX = nextMoveX == 1;
        }

        if (collision.tag == "ScaleY")
        {
            nextMoveY *= -1;
        }


    }

    protected override void move()
    {
        rigid.velocity = new Vector2(nextMoveX * 3, nextMoveY * 2);

        //flip
        if (rigid.velocity.x != 0)
        {
            spriteRenderer.flipX = nextMoveX == 1;
        }
    }
}
