using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("MapSelectScene");
    }

    public void QuitGame()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
