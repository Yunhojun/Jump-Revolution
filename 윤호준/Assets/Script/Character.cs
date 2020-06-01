using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float moveSpeed = 3f;
    private Rigidbody2D rigid;
    [SerializeField]
    private float jumpForce = 10f;
    private int jumpCount = 1;
    private int maxJump = 1;
    bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 velocity = new Vector3(hor, ver, 0).normalized * Time.deltaTime * moveSpeed;
        transform.Translate(velocity);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)&&(jumpCount > 0))
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            tab();
        }

        if (Input.GetKey(KeyCode.P))
        {
            respwan();
        }

    }

    public void tab()
    {
        transform.position = transform.position + Vector3.up * 20;
    }

    public void jump()
    {
        print("jump");
        jumped = true;
        rigid.Sleep();
        rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumpCount--;
    }

    public void InitJumpCount()
    {
        jumpCount = maxJump;
    }

    public void respwan()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor" && this.transform.position.y - collision.transform.position.y > 0)
        {
            print(collision.gameObject.name);
            GetComponentInParent<Character>().InitJumpCount();
            jumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (jumpCount == 1 && !jumped)
        {
            jumpCount--;
        }
    }
}
