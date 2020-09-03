using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBullet : bullet
{
    public GameObject TeleportEffect;
    public Vector2 teleportPos;
    public Rigidbody2D portalPos;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        teleportPos = portalPos.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().Stun(0.5f);
            collision.gameObject.GetComponent<Rigidbody2D>().position = teleportPos;
            Instantiate(TeleportEffect, teleportPos, Quaternion.Euler(-90, 0, 0));
            DestroyBullet();
            hit = true;
        }
    }
}
