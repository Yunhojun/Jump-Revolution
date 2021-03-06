﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HorizontalDart : MonoBehaviour
{
    public float distance;
    Rigidbody2D rigid;
    public Vector2 Velocity = new Vector2(10.0f,0);
    private Vector2 spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spawnPoint = rigid.position;
        rigid.velocity = Velocity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(rigid.position.x - spawnPoint.x) > distance)
        {
            StartCoroutine(VelocityShift());
        }
        else
        {
            rigid.velocity =new Vector2(0.01f,0) + rigid.velocity.normalized * Velocity.x;
        }

        rigid.rotation += 10;
    }

    IEnumerator VelocityShift()
    {
        rigid.velocity = new Vector2(rigid.velocity.x * -1, 0);
        yield return new WaitForSeconds(0.5f);
    }

}
