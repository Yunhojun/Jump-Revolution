using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    float hor = 0;
    float temp = 0;
    Rigidbody2D rigid = null;
    float time = 0;
    [SerializeField]
    float value = 1f; //미끄러지는 정도, 낮을 수록 더 잘 미끄러짐, 0보다는 커야 함
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.y < 0f)
        {
            rigid = collision.rigidbody;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rigid != null)
        {
            if (hor == 1 || hor == -1)
            {
                temp = hor;
                time = 0;
            }
            else if (hor == 0)
            {
                time += Time.deltaTime;
                rigid.velocity = new Vector2(Mathf.Lerp(5 * temp, 0, value * time), rigid.velocity.y);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        time = 0;
        temp = 0;
        rigid = null;
    }
}
