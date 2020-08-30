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


    /* protected override void Think()
     {

     }
     */



    protected override void move()
    {
        rigid.velocity = new Vector2(nextMoveX * 3
        , nextMoveY * 2);

        //flip
        if (rigid.velocity.x != 0)
        {
            spriteRenderer.flipX = nextMoveX == 1;
        }
    }


    private void Update()
    {
        StartCoroutine(VelocityShiftX());
        StartCoroutine(VelocityShiftY());

    }

    public override void tread(PlayerMove p)
    {
        p.Jump();
        Destroy();
    }

    IEnumerator VelocityShiftX()
    {
        if (Mathf.Abs(rigid.position.x - spawnPoint.x) > scaleX && shiftXOn == true)
        {
            Debug.Log("Shiftx");
            nextMoveX *= -1;
            shiftXOn = false;
            yield return new WaitForSeconds(1f);

        }
        else
        {
            shiftXOn = true;
            //yield return new WaitForSeconds(0.5f);
        }


    }

    IEnumerator VelocityShiftY()
    {
        if (Mathf.Abs(rigid.position.y - spawnPoint.y) > scaleY && shiftYOn == true)
        {
            Debug.Log("Shifty");
            nextMoveY *= -1;
            shiftYOn = false;
            yield return new WaitForSeconds(0.5f);

        }
        else
        {
            shiftYOn = true;
            //yield return new WaitForSeconds(0.5f);
        }

    }
    /*public override void tread(PlayerMove p)
   {
       p.Jump();
       Destroy();
   }*/





}
