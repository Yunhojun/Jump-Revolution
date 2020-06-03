using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField]
    float cycle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cycle += Time.deltaTime;
        if(cycle >= 2f)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            GetComponent<BoxCollider2D>().enabled = !GetComponent<BoxCollider2D>().enabled;
            cycle = 0;
        }
    }
}