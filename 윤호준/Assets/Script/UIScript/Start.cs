using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class Start : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene(GameManager.presentSceneNum);
    }

    
}
