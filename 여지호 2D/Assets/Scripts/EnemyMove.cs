using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Think();
    }

    void FixedUpdate()
    {
        // Move Enemy
        rigid.velocity = new Vector2(nextMove,rigid.velocity.y);

        // Platform Check
        Vector2 frontVector = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);
        Debug.DrawRay(frontVector, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVector, Vector2.down, 1 ,LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }

        // filp
        if(rigid.velocity.x != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        // Animation
        anim.SetInteger("WalkingSpeed", nextMove);

    }

    void Think()
    {
        // Set Random
        nextMove = Random.Range(-1, 2);

        Invoke("Think", Random.Range(1,3));
    }

    void Turn()  // 맵 끝에 갈때 발동하는 함수
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Think();
    }
}
