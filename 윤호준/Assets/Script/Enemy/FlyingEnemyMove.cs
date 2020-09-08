using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FlyingEnemyMove : EnemyMove
{
    protected float nextMoveX = 1;
    protected float nextMoveY = 1;

    public float scaleX = 12f;
    public float scaleY = 1f;

    public bool shiftXOn = true;
    public bool shiftYOn = true;


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
        if ((Mathf.Abs(rigid.position.x - spawnPoint.x) > scaleX) && (shiftXOn == true))
        {
            StartCoroutine(VelocityShiftX());
        }
        else if ((Mathf.Abs(rigid.position.y - spawnPoint.y) > scaleY) && (shiftYOn == true))
        {
            StartCoroutine(VelocityShiftY());
        }
    }

    public override void tread(PlayerMove p)
    {
        base.tread(p);
    }

    IEnumerator VelocityShiftX()
    {
        nextMoveX *= -1;
        Debug.Log("ShiftX");
        shiftXOn = false;
        yield return new WaitForSeconds(1f);
        shiftXOn = true;
    }

    IEnumerator VelocityShiftY()
    {
        nextMoveY *= -1;
        Debug.Log("ShiftY");
        shiftYOn = false;
        yield return new WaitForSeconds(0.5f);
        shiftYOn = true;
    }
}
