using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : bullet
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().stun(2f);
            DestroyBullet();
            collisionOn = false;
        }
    }
}
