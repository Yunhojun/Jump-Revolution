using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 5); // ("함수이름", "초") 주어진 시간이 지난 뒤 지정된 함수를 실행하는 함수
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        // Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.3f, rigid.position.y);      
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider == null)
            {
              Turn();
            }
        

        }

    //재귀 함수
    void Think()
    {
        //set Next Active
       nextMove = Random.Range(-1, 2); // -1은 최소에포함, 1은 최대에 포함이 안되므로 2를 써주어야함

        

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        // Flip sprite
        if(nextMove !=0)
            spriteRenderer.flipX = nextMove == 1;

        //Recusive (보통 코드 맨아래 작성)
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", 5);

    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }
}
