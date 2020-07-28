using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpPower = 20f;
    private Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private bool isLadder;
    private int jumpCount = 1;
    private const int maxJump = 1;
    private bool jumped;
    private bool tread;
    private bool stuned;
    private Transform Character;
    RaycastHit2D rayHitRight;
    RaycastHit2D rayHitLeft;
    private float stunTime;

    // Start is called before the first frame update
    void Awake()
    {
        tread = false;
        stuned = false;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Character = GetComponent<Transform>();
    }
    
   void Update()
   {

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jump();
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
            sit();

        if(Input.GetKeyUp(KeyCode.LeftControl))
            stand();
                 
             
   }

    void FixedUpdate()
    {
        //Move by Key Control
        float hor = Input.GetAxisRaw("Horizontal");
        move(hor);

        Debug.DrawRay(transform.position + Vector3.right * 0.3f + Vector3.down * 0.4f, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(transform.position + Vector3.left * 0.3f + Vector3.down * 0.4f, Vector3.down, new Color(0, 1, 0));
        rayHitRight = Physics2D.Raycast(transform.position + Vector3.right * 0.3f + Vector3.down * 0.4f, Vector3.down, 1, LayerMask.GetMask("Enemy"));
        rayHitLeft = Physics2D.Raycast(transform.position + Vector3.left * 0.3f + Vector3.down * 0.4f, Vector3.down, 1, LayerMask.GetMask("Enemy"));
        tread = (rayHitRight.collider || rayHitLeft.collider);

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
            rigid.gravityScale = 4f;
        }

        if (stuned)
        {
            stunTime += Time.deltaTime;
            if (stunTime > 2f)
            {
                stuned = false;
                stunTime = 0;
            }
        }
    }

    public void move(float hor)
    {
        if (!stuned)
        {
            Vector3 velocity = new Vector3(hor, 0, 0).normalized * Time.deltaTime * moveSpeed;
            transform.Translate(velocity);
        }
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

    public void stand()
    {
        Character.transform.localScale = new Vector3(1, 1, 0);
        Character.transform.Translate(new Vector3(0, 0.25f, 0));
        Debug.Log("stand");
    }

    public void sit()
    {
        Character.transform.localScale = new Vector3(1, 0.5f, 0);
        Character.transform.Translate(new Vector3(0, -0.25f, 0));
        Debug.Log("sit");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        // 몬스터와 충돌시 발생 이벤트
        if (collision.gameObject.tag == "Enemy")//collide with monster
        {
            if(tread == false)
            {
                onDamaged(collision.transform.position);
            }
            else
            {
                EnemyMove e = collision.gameObject.GetComponent<EnemyMove>();
                e.tread(this);
            }
        }

        // 맵과 충돌시 발생 이벤트
        if (collision.gameObject.tag == "floor" &&
            this.transform.position.y - collision.transform.position.y > 0)//collide with floor
        {
            InitJumpCount();
            stuned = false;
            jumped = false;
            anim.SetBool("isJumping", false);
        }     
        

        // 총알과 충돌시 발생 이벤트
        //if(collision.gameObject.tag == "bullet") // collide with bullet
        //{
        //    //onDamaged(collision.transform.position);
        //}

        //if (collision.gameObject.tag == "iceBullet") // collide with icebullet
        //{
        //    stun();
        //}

        //if (collision.gameObject.tag == "teleportBullet") // collide with teleportBullet
        //{
        //    transform.position = collision.gameObject.GetComponent<TeleportBullet>().GetSpawnpoint() + Vector2.up*3;
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)//when fall from floor
    {
        if(collision.gameObject.tag == "floor")
        {
            if (jumpCount >= 1 && !jumped)
            {
                jumpCount--;
            }
        }
    }

    public void onDamaged(Vector2 targetPos)
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

    public Rigidbody2D GetRig()
    {
        return rigid;
    }

    public void stun()
    {
        stuned = true;
        
    }

    public void SetMoveSpeed(float f)
    {
        moveSpeed = f;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

}
