using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdder : MonoBehaviour
{
    public ScoreManager scoreManager;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            scoreManager.addScore(5);
            Debug.Log(scoreManager.GetScore());
        }
    }
}
