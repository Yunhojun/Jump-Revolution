using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator Animator;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Jump
        if(Input.GetButtonDown("Jump") && !Animator.GetBool("IsJumping")) {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        Animator.SetBool("IsJumping", true);
        }
        //Stop Speed
        if(Input.GetButtonUp("Horizontal")){
    
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.1f, rigid.velocity.y);
        }
        //방향전환
        if(Input.GetButtonDown("Horizontal")){
    
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if(Mathf.Abs(rigid.velocity.x) < 0.1 )
        Animator.SetBool("IsWalk", false);
        else
        Animator.SetBool("IsWalk", true);
    }

    void FixedUpdate()
    {
        
        //Move By Control
        float h = Input.GetAxisRaw("Horizontal");
        
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)//RIght Max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))//Left Max speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        //Landing Platform
        if(rigid.velocity.y < 0){
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if(rayHit.collider != null){
            if(rayHit.distance < 0.5f)
            Animator.SetBool("IsJumping", false);
            }
        }
        if(isLadder)
        {
            rigid.gravityScale = 0;
            float vel = Input.GetAxis("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x, vel * maxSpeed);
        }
        else
        {
            rigid.gravityScale = 3f;
        }
    }
    public bool isLadder;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}
