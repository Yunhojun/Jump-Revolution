using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpPower = 20f;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private bool isLadder;
    private int jumpCount = 1;
    private const int maxJump = 1;
    private bool jumped;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
       
    }
    
   void Update()
   {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jump();
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
        }
   }

    void FixedUpdate()
    {
        //Move by Key Control
        float hor = Input.GetAxisRaw("Horizontal");
        move(hor);

        //Landing Platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }

            }
        }

        //on ladder
        if (isLadder)
        {
            rigid.gravityScale = 0;
            float ver = Input.GetAxisRaw("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x, ver * moveSpeed);
        }
        else
        {
            rigid.gravityScale = 3f;
        }
    }

    public void move(float hor)
    {
        Vector3 velocity = new Vector3(hor, 0, 0).normalized * Time.deltaTime * moveSpeed;
        transform.Translate(velocity);
        if (hor == 0)
        {
            anim.SetBool("isWalking", false);            
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }

    public void jump()
    {
        rigid.Sleep();
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
        jumped = true;
        jumpCount--;
    }

    public void InitJumpCount()
    {
        jumpCount = maxJump;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")//collide with monster
        {
            onDamaged(collision.transform.position);
        }
        if (collision.gameObject.tag == "floor" &&
            this.transform.position.y - collision.transform.position.y > 0)//collide with floor
        {
            InitJumpCount();
            jumped = false;
            anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)//when fall from floor
    {
        if (jumpCount >= 1 && !jumped)
        {
            jumpCount--;
        }
    }

    void onDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x-targetPos.x > 0 ? 1 :-1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

        anim.SetTrigger("deDamaged");

        Invoke("OffDamaged", 1.5f);       
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            InitJumpCount();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}
