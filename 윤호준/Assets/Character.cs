using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float moveSpeed = 3f;
    private Rigidbody2D rigid;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    protected int jumpCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        Vector3 velocity = new Vector3(hor, ver, 0).normalized * Time.deltaTime *moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space)&&(jumpCount > 0))
        {
            jump();
        }

        transform.Translate(velocity);
    }

    public void jump()
    {
        rigid.Sleep();
        rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumpCount--;
    }

    public void InitJumpCount()
    {
        jumpCount = 2;
    }
}
