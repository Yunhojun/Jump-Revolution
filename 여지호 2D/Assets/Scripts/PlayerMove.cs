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

    }
}
