using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationBox : MonoBehaviour
{
    public static bool boxOn = false;
    public GameObject canvas2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (boxOn == false)
            {
                canvas2.SetActive(true);
                boxOn = true;
            }
            else
            {
                canvas2.SetActive(false);
                boxOn = false;
            }
        }
        
    }
}
