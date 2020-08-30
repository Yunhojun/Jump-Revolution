using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VerticalDart : MonoBehaviour
{
    public float distance;
    Rigidbody2D rigid;
    private Vector2 Velocity = new Vector2(0, 10.0f);
    private Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spawnPoint = rigid.position;
        rigid.velocity = Velocity;
        rigid.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(rigid.position.y - spawnPoint.y) > distance)
        {
            StartCoroutine(VelocityShift());
        }
        else
        {
            rigid.velocity = rigid.velocity.normalized * Velocity.y;
        }
    }

    IEnumerator VelocityShift()
    {
        rigid.velocity = new Vector2(0,rigid.velocity.y * -1);
        yield return new WaitForSeconds(0.5f);
    }

}
