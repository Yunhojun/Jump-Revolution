using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D Player;
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public float jumpPower;

    private void Awake()
    {
        Player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() // 연속적인 입력시 픽스드업데이트
    {
        //Move by Control
        float h = Input.GetAxisRaw("Horizontal");
        Player.AddForce(Vector2.right * h, ForceMode2D.Impulse);


        //Max Speed
        if (Player.velocity.x > maxSpeed)
        {
            Player.velocity = new Vector2(maxSpeed, Player.velocity.y);
        } // right Max Speed
        else if (Player.velocity.x < maxSpeed * (-1))
        {
            Player.velocity = new Vector2(maxSpeed * (-1), Player.velocity.y);
        } // left Max Speed

        //Landing Platform (Raycast와 Layer) 매우 중요
        Debug.DrawRay(Player.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(Player.position, Vector2.down, 1,LayerMask.GetMask("Platform"));
        if(Player.velocity.y < 0)
        {
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }

    private void Update() // 단발적인 입력은 업데이트에 해놓자
    {
        // Jump (+ 무한점프 금지)
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            Player.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        // Stop speed
        if(Input.GetButtonUp("Horizontal"))
        {
            Player.velocity = new Vector2(Player.velocity.normalized.x * 0.5f, Player.velocity.y);
        }

        // Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        // Animation
        if(Mathf.Abs(Player.velocity.x) < 0.3)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }


    }
}
