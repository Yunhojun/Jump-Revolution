using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBullet : bullet
{
    public Vector2 spawnPoint;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().position = spawnPoint + Vector2.up * 3;
            DestroyBullet();
            collisionOn = false;
        }
    }
}
