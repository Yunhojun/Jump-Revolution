using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f; //이동속도
    [SerializeField]
    public float jumpPower = 21f; //점프력
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    private bool isLadder; //사다리에 있는지 여부
    public int jumpCount = 1; //점프 횟수
    public int dashCount = 0;  // 대쉬 횟수
    private const int maxJump = 1; //최대 점프 횟수, 2단 점프를 가능하게 하려면 2로 수정
    private bool dashed;
    private bool tread;  // 밟기 가능한 상태 인지 여부
    private bool stuned; // 이동가능한 상태 인지 여부
    private Transform Character; // 대쉬 할때 필요한 위치 변수
    private float hor; //좌우 입력
    private float ver; //사다리용 상하 입력
    RaycastHit2D[] enemyRay = new RaycastHit2D[2]; //적을 밟을 수 있는지 판단하는 레이

    // Start is called before the first frame update
    void Awake()
    {
        hor = 0;
        ver = 0;
        tread = false;
        stuned = false;
        isLadder = false;

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Character = GetComponent<Transform>();
    }
    
   void Update() // 키 입력 관련
   {
        // Jump Code
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0) // 점프
        {
            jump();
        }

        // 이동
        hor = Input.GetAxisRaw("Horizontal"); //좌우이동

        // 사다리 수직이동
        ver = Input.GetAxisRaw("Vertical");

        // Dash Code
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.LeftArrow) && dashCount > 0)
        {
            dashLeft();
        }
        else if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.RightArrow) && dashCount > 0)
        {
            dashRight();
        }
        else if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.UpArrow) && dashCount > 0)
        {
            dashUp();
        }
        else if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.DownArrow) && dashCount > 0)
        {
            dashDown();
        }

        ////Direction Sprite
        //if (Input.GetButton("Horizontal"))
        //{
        //    spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
        //}

        // 앉기 버튼
        if (Input.GetKeyDown(KeyCode.LeftControl))
            sit();

        // 일어서기 버튼
        if(Input.GetKeyUp(KeyCode.LeftControl))
            stand();
                 
             
   }

    void FixedUpdate() // 이동관련
    {
        //Move by Key Control
        move(hor);

        enemyRay[0] = Physics2D.Raycast(transform.position + Vector3.right * 0.3f + Vector3.down * 0.4f, Vector3.down, 1, LayerMask.GetMask("Enemy"));
        enemyRay[1] = Physics2D.Raycast(transform.position + Vector3.left * 0.3f + Vector3.down * 0.4f, Vector3.down, 1, LayerMask.GetMask("Enemy"));
        tread = (enemyRay[0].collider || enemyRay[1].collider);

        //on ladder
        if (isLadder)
        {
            Vector3 velocity = Vector2.up * ver * moveSpeed * Time.deltaTime;
            transform.Translate(velocity);
        }
    }
    public void dashUp()
    {
        rigid.Sleep();
        Character.Translate(new Vector2 (0, 4));
        dashCount--;
    }

    public void dashLeft()
    {
        rigid.Sleep();
        Character.Translate(new Vector2 (-4, 0));
        dashCount--;
    }
    public void dashRight()
    {
        rigid.Sleep();
        Character.Translate(new Vector2 (4, 0));
        dashCount--;
    }
    public void dashDown()
    {
        rigid.Sleep();
        Character.Translate(new Vector2 (0, -4));
        dashCount--;
    }

    public void move(float hor)//좌우 이동
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
            spriteRenderer.flipX = (hor == -1);
            anim.SetBool("isWalking", true);
        }
    }

    public void jump() // 점프
    {
        rigid.Sleep();
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
        jumpCount--;
    }

    public void InitJumpCount() // 점프 횟수를 초기화
    {
        jumpCount = maxJump;
    }

    public void stand() // 일어서기
    {
        transform.localScale = new Vector3(1, 1, 0);
        transform.Translate(new Vector3(0, 0.25f, 0));
        Debug.Log("stand");
    }
    public void sit() // 앉기
    {
        transform.localScale = new Vector3(1, 0.5f, 0);
        transform.Translate(new Vector3(0, -0.25f, 0));
        Debug.Log("sit");
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")//collide with monster
        {
            if (tread == false)//적에게 맞았을 때
            {
                onDamaged(collision.transform.position);
            }
            else//적을 밟았을 때
            {
                EnemyMove e = collision.gameObject.GetComponent<EnemyMove>();
                e.tread(this);
            }
        }
        if (collision.gameObject.tag == "floor")//collide with floor
        {
            if (collision.relativeVelocity.y >= 0f)//바닥에 착지
            {
                InitJumpCount();
                stuned = false;
                anim.SetBool("isJumping", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)//when fall from floor
    {
        if(collision.gameObject.tag == "floor")
        {
            if (jumpCount >= 1 && !isLadder)
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
            rigid.Sleep();
            rigid.gravityScale = 0;
            InitJumpCount();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            rigid.gravityScale = 4f;
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

    public float GetHorizon()
    {
        return hor;
    }
}
