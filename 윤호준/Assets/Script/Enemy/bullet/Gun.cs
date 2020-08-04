using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gun : EnemyMove
{
    Rigidbody2D rigidGun;
    public Rigidbody2D objRigid;
    Rigidbody2D objIns;
    public float distance;
    PlayerMove p = null;
    //public float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidGun = GetComponent<Rigidbody2D>();
        objIns = Instantiate<Rigidbody2D>(objRigid, transform.position - new Vector3(1, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    new private void FixedUpdate()
    {
        if (objIns.GetComponent<bullet>().collisionOn == true)
        {
            objIns.velocity = new Vector2(-5, 0);
            if (Mathf.Abs((spawnPoint.x - objIns.position.x)) > distance)
            {
                if (this.destroyed == false)
                {
                    objIns.GetComponent<bullet>().DestroyBullet();
                    StartCoroutine(BulletRespawnTerm(0.5f));
                }
                else
                {
                    objIns.velocity = Vector2.zero;
                    objIns.GetComponent<bullet>().DestroyBullet();
                }
            }
        }
        else
        {
            objIns.velocity = Vector2.zero;
            objIns.position = spawnPoint;
            if (destroyed == false)
            {
                CancelInvoke("RespawnBullet");
                Invoke("RespawnBullet", 3f);
                objIns.GetComponent<bullet>().collisionOn = true;
            }
        }

    }

    public void RespawnBullet()
    {
        //objIns.position = this.spawnPoint;
        objIns.GetComponent<Transform>().position = this.spawnPoint;
        objIns.GetComponent<Collider2D>().enabled = true;
        objIns.GetComponent<bullet>().collisionOn = true;

    }

    public override void tread(PlayerMove p)
    {
        base.tread(p);
        this.p = p;
        CancelInvoke("RespawnBullet");
        Invoke("RespawnBullet", 3.5f);
    }

    IEnumerator BulletRespawnTerm(float second)
    {
        yield return new WaitForSeconds(second);
        RespawnBullet();
    }
}
