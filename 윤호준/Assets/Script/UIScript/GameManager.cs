using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timeText;
    public Text clearText;
    private int time = 0;
    public static bool fin = false;
    public static bool isPause = false;
    public GameObject Pause;
    public static string[] scenes = new string[20];
    public static int presentSceneNum = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        SceneSetting();
        StartCoroutine(CountUp());
    }

    private void FixedUpdate()
    {
        if(fin == true)
        {
            StartCoroutine(SceneChange());
        }


        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPause == false)
            {
                Pause.SetActive(true);
                Time.timeScale = 0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                isPause = true;
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                Pause.SetActive(false);
                isPause = false;
            }
        }
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
        clearText.enabled = true;
    }

    IEnumerator SceneChange() // 씬 전환
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scenes[presentSceneNum + 1]);
        fin = false;
    }

    private void SceneSetting()
    {
        scenes[0] = "Title";
        scenes[1] = "여지호";
        scenes[2] = "윤호준";
    }

    
}
