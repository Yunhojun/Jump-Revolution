using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TeleportGun : EnemyMove
{
    Rigidbody2D rigidGun;
    public Rigidbody2D objRigid;
    Rigidbody2D objIns;
    public float distance;
    PlayerMove p = null;
    bullet bullet = null;
    Transform bulletTrf = null;
    Collider2D bulletCol = null;
    bool fliped = false;
    public Rigidbody2D portalPos;
    private Coroutine coVar;
    //public float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidGun = GetComponent<Rigidbody2D>();
        objIns = Instantiate<Rigidbody2D>(objRigid, transform.position - new Vector3(1, 0), Quaternion.identity);
        bullet = objIns.GetComponent<bullet>();
        bulletTrf = objIns.GetComponent<Transform>();
        bulletCol = objIns.GetComponent<Collider2D>();
        fliped = GetComponent<SpriteRenderer>().flipX;
        objIns.GetComponent<TeleportBullet>().portalPos = portalPos;
    }

    // Update is called once per frame
    void Update()
    {

    }

    new private void FixedUpdate()
    {
        if (bullet.collisionOn == true)
        {
            if (!fliped)
            {
                objIns.velocity = new Vector2(-5, 0);
            }
            else
            {
                objIns.velocity = new Vector2(5, 0);
            }
            if (Mathf.Abs((spawnPoint.x - objIns.position.x)) > distance)
            {
                if (this.destroyed == false)
                {
                    bullet.DestroyBullet();
                    StartCoroutine(BulletRespawnTerm(0.25f));
                }
                else
                {
                    objIns.velocity = Vector2.zero;
                    bullet.DestroyBullet();
                }
            }
        }
        else
        {
            objIns.velocity = Vector2.zero;
            objIns.position = spawnPoint;
            if (destroyed == false)
            {
                if (bullet.hit == true)
                {
                    coVar = StartCoroutine(BulletRespawnLongTerm(3f));
                    bullet.hit = false;
                }

            }

        }

    }

    public void RespawnBullet()
    {
        bulletTrf.position = this.spawnPoint;
        bulletCol.enabled = true;
        bullet.collisionOn = true;
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
        if (destroyed == false)
        {
            anim.SetBool("Shot", false);
            yield return new WaitForSeconds(0.25f);
            RespawnBullet();
        }
        else
        {
            StopCoroutine(coVar);
            yield return new WaitForSeconds(5f);
            anim.SetBool("Shot", false);
            yield return new WaitForSeconds(0.25f);
            RespawnBullet();
        }
    }
}
