using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FlyingEnemyMove : MonoBehaviour
{
    public int nextMoveX = 1;
    public int nextMoveY = 1;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public Vector2 spawnPoint;




    void Start()
    {

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPoint = rigid.position;

    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        //move
        rigid.velocity = new Vector2(nextMoveX * 3, nextMoveY * 2);

        //flip
        if (rigid.velocity.x != 0)
        {
            spriteRenderer.flipX = nextMoveX == 1;
        }
    }




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

}
