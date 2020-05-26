using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D Player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 이동 구현
        float inputX = Input.GetAxis("Horizontal");
        Vector2 originalState = Player.position;
        Vector2 move = new Vector2(inputX * speed, originalState.y);
        Player.position += move * Time.deltaTime;

        // 플레이어 점프 구현
        if (Input.GetButtonDown("Jump"))
        {
            if (Player.velocity.y <= 0.0001)
            {
                Vector2 jump = new Vector2(0, 300);
                Player.AddForce(jump);
            }
        }

    }
}
