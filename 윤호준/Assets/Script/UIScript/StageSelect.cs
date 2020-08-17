using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Map map;
    public Rank rank;
    public static bool isRank = false;
    public GameObject RankBoard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMap()
    {
        if(isRank == false)
        {
            SceneManager.LoadScene(map.GetMapName());
            GameManager.presentScene = map.GetMapName();
        }
        else
        {
            RankBoard.SetActive(true);
            rank.Load();
        }
    }

}
