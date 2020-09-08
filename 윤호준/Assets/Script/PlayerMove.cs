using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed { get; set; } = 5f; //이동속도
    public float jumpPower = 21f; //점프력
    public Rigidbody2D rigid { get; private set; }
    SpriteRenderer spriteRenderer;
    Animator anim;
    private bool isLadder; //사다리에 있는지 여부
    public int jumpCount { get; set; } = 1; //점프 횟수
    public int dashCount { get; set; } = 0;  // 대쉬 횟수
    private const int maxJump = 1; //최대 점프 횟수, 2단 점프를 가능하게 하려면 2로 수정
    private bool dashed;
    private bool stuned { get; set; } // 이동가능한 상태 인지 여부
    private Transform Character; // 대쉬 할때 필요한 위치 변수
    public float hor { get; private set; } //좌우 입력
    private float ver; //사다리용 상하 입력
    private Vector2 savePos;
    public GameObject SavePoint;
    public GameObject DashEffect;
    public GameObject StunEffect;
    public Stun StunCheck;

    // 몬스터 밟았을 때 지속시간 판단
    public bool highJumpOn = false; //현재 하이점프 중인지 판단
    public bool lowJumpOn = false; //현재 로우점프 중인지 판단
    public bool isNormalSpeed { get; set; } = true;

    public Coroutine co = null;

    private float currrentSpeed = 5f;
    private bool isSit = false;

    // Start is called before the first frame update
    void Awake()
    {
        hor = 0;
        ver = 0;
        stuned = false;
        isLadder = false;
        StunCheck = GetComponentInChildren<Stun>();


        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Character = GetComponent<Transform>();
        savePos = rigid.position;
    }
    
   void Update() // 키 입력 관련
   {        
        // 이동
        hor = Input.GetAxisRaw("Horizontal"); //좌우이동

        // 사다리 수직이동
        ver = Input.GetAxisRaw("Vertical");

        // Jump Code
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0) // 점프
        {
            if (!stuned)
            {
                Jump();
            }
        }

        // Dash Code
        if (Input.GetKeyDown(KeyCode.X) && dashCount > 0)
        {
            if (ver != 0)
            {
                DashVer();
            }
            else if (hor != 0)
            {
                DashHor();
            }
            
        }

        if (Input.GetKey(KeyCode.F) && jumpCount == maxJump)
        {
            Save();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Load();
        }


        // 앉기 버튼
        if (Input.GetKey(KeyCode.LeftControl)&&jumpCount==maxJump&&!isSit)
            Sit();

        // 일어서기 버튼
        if(Input.GetKeyUp(KeyCode.LeftControl)&&isSit)
            Stand();
                 
             
   }

    void FixedUpdate() // 이동관련
    {
        //Move by Key Control
        Move(hor);        

        //on ladder
        if (isLadder)
        {
            Vector3 velocity = Vector2.up * ver * moveSpeed * Time.deltaTime;
            transform.Translate(velocity);
        }
    }

    public void Move(float hor)//좌우 이동
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

    public void Jump() // 점프
    {
        rigid.Sleep();
        SoundScript.Inst.jumpPlayer();
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
        jumpCount--;
    }

    public void InitJumpCount() // 점프 횟수를 초기화
    {
        jumpCount = maxJump;
    }

    public void Stand() // 일어서기
    {
        moveSpeed = currrentSpeed;
        isSit = false;
        transform.localScale = new Vector3(1, 1, 0);
        transform.Translate(new Vector3(0, 0.25f, 0));
    }
    public void Sit() // 앉기
    {
        currrentSpeed = moveSpeed;
        moveSpeed = 0f;
        isSit = true;
        transform.localScale = new Vector3(1, 0.5f, 0);
        transform.Translate(new Vector3(0, -0.25f, 0));
    }
    
    public void DashVer()
    {
        anim.SetBool("isDashing", true);
        Instantiate(DashEffect, transform.position, transform.rotation);
        rigid.Sleep();
        SoundScript.Inst.dashPlayer();
        Character.Translate(new Vector2(0, 4 * ver));
        dashCount--;
        Invoke("Dashdelay", 0.5f);
    }

    public void DashHor()
    {
        anim.SetBool("isDashing", true);
        Instantiate(DashEffect, transform.position, transform.rotation);
        rigid.Sleep();
        SoundScript.Inst.dashPlayer();
        Character.Translate(new Vector2(4 * hor, 0));
        dashCount--;
        Invoke("Dashdelay", 0.5f);
    }

    public void Dashdelay()
    {
        anim.SetBool("isDashing", false);
    }

    public void Stun(float t)
    {
        StartCoroutine(StunCoroutine(t));
        Instantiate(StunEffect, transform.position, Quaternion.identity);
    }

    private IEnumerator StunCoroutine(float t)
    {
        stuned = true;
        StunCheck.StunOn();
        yield return new WaitForSeconds(t);
        stuned = false;
        StunCheck.StunOff();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))//collide with monster
        {
            OnDamaged(collision.transform.position);
        }
        if (collision.gameObject.CompareTag("floor"))//collide with floor
        {
            if (collision.GetContact(0).normal == Vector2.up && collision.GetContact(1).normal == Vector2.up)//바닥에 착지
            {
                InitJumpCount();
                anim.SetBool("isJumping", false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            if(rigid.velocity.y == 0)
            {
                InitJumpCount();
                anim.SetBool("isJumping", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)//when fall from floor
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            if (jumpCount >= 1 && !isLadder)
            {
                jumpCount--;
            }
        }
    }

    public void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        Stun(0.3f);
        int dirc = transform.position.x-targetPos.x > 0 ? 1 :-1;
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);

        anim.SetTrigger("deDamaged");

        Invoke("OffDamaged", 1.5f);       
    }

    public void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")&&ver != 0)
        {
            if (!stuned)
            {
                isLadder = true;
                rigid.Sleep();
                rigid.gravityScale = 0;
                InitJumpCount();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder") && ver != 0)
        {
            if (!stuned)
            {
                isLadder = true;
                rigid.Sleep();
                rigid.gravityScale = 0;
                InitJumpCount();
            }
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

    public void Save()
    {
        savePos = rigid.position;
        SavePoint.transform.position = savePos + Vector2.up;
    }

    public void Load()
    {
        rigid.position = savePos;
    }

    public Coroutine GetCoroutine()
    {
        return co;
    }

    public void SetCoroutine(float t)
    {
        co = StartCoroutine(RecoverMoveSpeed(t));
    }

    public IEnumerator RecoverMoveSpeed(float t)
    {
        yield return new WaitForSeconds(t);

        currrentSpeed = 5f;
        isNormalSpeed = true;
        if (!isSit)
        {
            moveSpeed = 5f;
        }
    }
}
