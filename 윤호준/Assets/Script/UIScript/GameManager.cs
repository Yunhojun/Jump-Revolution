using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public GameObject soundManagerCanvas;
    public Text timeText;
    public GameObject clear;
    private int time = 0;
    public static bool fin = false;
    public static bool isPause = false;
    public GameObject Pause;
    public static string presentScene;
    private bool inputNameBool = false;
    public InputField inputName;
    public Rank rank;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountUp());
        clear.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(fin == true && inputNameBool == true)
        {
            StartCoroutine(SceneChange());
        }


        
    }

    private void Update()
    {
        PauseUI();
    }

    IEnumerator CountUp() // 타임어택 모드
    {
        while (!fin)
        {
            
            yield return new WaitForSeconds(1f);
            time += 1;
            timeText.text = "Time : " + time;
        }


        timeText.text = "Time : " + time;
        clear.SetActive(true);
    }

    IEnumerator SceneChange() // 씬 전환
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MapSelectScene");
        fin = false;
        inputNameBool = false;
    }


    public void PauseUI()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPause == false)
            {
                Pause.SetActive(true);
                soundManagerCanvas.SetActive(true);
                Time.timeScale = 0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                isPause = true;
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                Pause.SetActive(false);
                soundManagerCanvas.SetActive(false);
                isPause = false;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(presentScene);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Pause.SetActive(false);
        isPause = false;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MapSelectScene");
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Pause.SetActive(false);
        isPause = false;
    }

    public void ContinueButton()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Pause.SetActive(false);
        isPause = false;
    }

    public void Save()
    {
        rank.mapNumChange();
        rank.SortAndSave();
        Debug.Log("세이브 성공");
        inputNameBool = true;
    }

    public int GetTime()
    {
        return time;
    }

}

