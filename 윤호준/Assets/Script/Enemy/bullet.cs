using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spawnPoint = rigid.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity == Vector2.zero)
        {
            if (rigid.velocity == Vector2.zero)
            {
                rigid.position = spawnPoint;
            }
        }
    }
}
