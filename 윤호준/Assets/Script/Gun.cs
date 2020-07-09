using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Rigidbody2D rigid;
    public Rigidbody2D objRigid;
    Rigidbody2D objIns;
    public float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        objIns = Instantiate<Rigidbody2D>(objRigid, transform.position - new Vector3(1, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        objIns.velocity = new Vector2(-5, 0);
        count += Time.deltaTime;
        if (count >= 3)
        {
            count = 0;
        }
    }
}
