using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public bool isStun;
    
    private SpriteRenderer stunSprite;

    void Awake() 
    {
        stunSprite = GetComponent<SpriteRenderer>();
    }

 
    void Update()
    {
       
    }

    public void StunOn()
    {
        stunSprite.enabled = true;
    }

    public void StunOff()
    {
        stunSprite.enabled = false;
    }
}

