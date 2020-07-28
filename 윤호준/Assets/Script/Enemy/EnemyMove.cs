using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    protected Rigidbody2D rigid;
    [SerializeField]
    protected int nextMove;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected Vector3 spawnPoint;
    protected bool destroyed;

    protected void Awake()
    {
        destroyed = false;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPoint = transform.position;

    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        //move
        if (!destroyed)
        {
            move();
        }
    }
    
    protected void Respwan()
    {
        transform.position = spawnPoint;
        rigid.gravityScale = 1;
        destroyed = false;
        GetComponent<Collider2D>().enabled = true;
    }

    protected void Destroy()
    {
        transform.position = transform.position + Vector3.back * 100;
        GetComponent<Collider2D>().enabled = false;
        rigid.gravityScale = 0;
        destroyed = true;
        Invoke("Respwan", 3f);
    }

    //재귀 함수
    protected virtual void Think()
    {
        //set Next Active
        nextMove = Random.Range(-1, 2); // -1은 최소에포함, 1은 최대에 포함이 안되므로 2를 써주어야함 

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        // Flip sprite
        if(nextMove !=0)
            spriteRenderer.flipX = (nextMove == 1);

        //Recusive (보통 코드 맨아래 작성)
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

    }

    protected virtual void move()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            nextMove *= -1;
            spriteRenderer.flipX = nextMove == 1;

            CancelInvoke();
            Invoke("Think", 5f);
        }
    }

    public virtual void tread(PlayerMove p)
    {
        p.jump();
        Destroy();
    }
}
