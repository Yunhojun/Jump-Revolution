using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField]
    float cycle = 2f;
    float time = 0f;
    SpriteRenderer sprite = null;
    BoxCollider2D col = null;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator LaserCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            sprite.enabled = !sprite.enabled;
            col.enabled = !col.enabled;
            time = 0;
        }
    }
}