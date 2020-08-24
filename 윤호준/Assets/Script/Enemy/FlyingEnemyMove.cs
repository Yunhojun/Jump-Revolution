using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FlyingEnemyMove : EnemyMove
{
    protected int nextMoveX = 1;
    protected int nextMoveY = 1;

    public float scaleX = 12f;
    public float scaleY = 1f;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("ScaleX"))
    //    {
    //        nextMoveX *= -1;
    //        spriteRenderer.flipX = nextMoveX == 1;
    //    }

    //    if (collision.CompareTag("ScaleY"))
    //    {
    //        nextMoveY *= -1;
    //    }
    //}

    protected override void Think()
    {
        
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

    private void Update()
    {
        if (Mathf.Abs(rigid.position.x - spawnPoint.x) > scaleX)
        {
            Debug.Log("x범위 초과");
            StartCoroutine(VelocityShiftX());
        }

        if (Mathf.Abs(rigid.position.y - spawnPoint.y) > scaleY)
        {
            Debug.Log("y범위 초과");
            StartCoroutine(VelocityShiftY());
        }

        else
        {
            //rigid.velocity = rigid.velocity.normalized * Velocity.x;
        }
    }

    IEnumerator VelocityShiftX()
    {
        //rigid.velocity = new Vector2(rigid.velocity.x * -1, rigid.velocity.y);
        nextMoveX *= -1;
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator VelocityShiftY()
    {
        //rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * -1);
        nextMoveY *= -1;
        yield return new WaitForSeconds(0.2f);
    }
}
