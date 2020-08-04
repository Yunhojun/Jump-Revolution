using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class bullet : MonoBehaviour
{
    protected Rigidbody2D rigid;
    public bool collisionOn;
    public bool hit;

    // Start is called before the first frame update
    protected void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collisionOn = true;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DestroyBullet()
    {
        transform.position = transform.position + Vector3.back * 100;
        GetComponent<Collider2D>().enabled = false;
        collisionOn = false;
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().onDamaged(transform.position);
            DestroyBullet();
            hit = true;
        }
    }


}
