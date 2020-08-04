using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Gun : EnemyMove
{
    Rigidbody2D rigidGun;
    public Rigidbody2D objRigid;
    Rigidbody2D objIns;
    public float distance;
    PlayerMove p = null;
    bool coroutineOn = false;
    //public float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
                    StartCoroutine(BulletRespawnTerm(0.25f));
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
                if (objIns.GetComponent<bullet>().hit == true)
                {
                    StartCoroutine(BulletRespawnLongTerm(3f));
                    objIns.GetComponent<bullet>().hit = false;
                }

            }

        }

    }

    public void RespawnBullet()
    {
        objIns.GetComponent<Transform>().position = this.spawnPoint;
        objIns.GetComponent<Collider2D>().enabled = true;
        objIns.GetComponent<bullet>().collisionOn = true;
        anim.SetBool("Shot", true);



    }

    public override void tread(PlayerMove p)
    {
        base.tread(p);
        this.p = p;
        StartCoroutine(BulletRespawnLongTerm(5.5f));
    }

    IEnumerator BulletRespawnTerm(float second)
    {
        anim.SetBool("Shot", false);
        yield return new WaitForSeconds(second);
        RespawnBullet();
    }

    IEnumerator BulletRespawnLongTerm(float second)
    {
        yield return new WaitForSeconds(second);
        anim.SetBool("Shot", false);
        yield return new WaitForSeconds(0.25f);
        RespawnBullet();
    }
}
