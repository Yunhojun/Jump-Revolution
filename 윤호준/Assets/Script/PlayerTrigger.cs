using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject Player;
    PlayerMove p;
    RaycastHit2D[] enemyRay = new RaycastHit2D[2]; //적을 밟을 수 있는지 판단하는 레이
    bool tread;

    // Start is called before the first frame update
    void Start()
    {
        p = Player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRay[0] = Physics2D.Raycast(transform.position + Vector3.right * 0.4f + Vector3.down * 0.51f, Vector3.down, 1, LayerMask.GetMask("Enemy", "FlyingMonster"));
        enemyRay[1] = Physics2D.Raycast(transform.position + Vector3.left * 0.4f + Vector3.down * 0.51f, Vector3.down, 1, LayerMask.GetMask("Enemy", "FlyingMonster"));
        tread = (enemyRay[0].collider || enemyRay[1].collider);
        Debug.DrawRay(transform.position + Vector3.left * 0.4f + Vector3.down * 0.4f, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.right * 0.4f + Vector3.down * 0.4f, Vector3.down);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyMove e = collision.GetComponent<EnemyMove>();
            if (tread)
            {
                if(e != null)
                {
                    e.tread(p);
                }
            }
        }
    }    
}
