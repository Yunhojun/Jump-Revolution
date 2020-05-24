using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{

    float speed = 5.0f;
    private  Rigidbody target;
    

    void Start()
    {
        target = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(inputX, 0, 0);
        target.position = target.position + movement * Time.deltaTime * speed;

        

        if (Input.GetButtonDown("Jump"))
        {
            target.AddForce(0, 300, 0);
        }
    }
}
