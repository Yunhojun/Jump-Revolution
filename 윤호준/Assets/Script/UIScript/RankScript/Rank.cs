using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public GameManager gmr;
    protected int[] times = new int[5];
    protected Dictionary<string, int> rankDict = new Dictionary<string, int>();
    protected int mapNum = 1;

    // 랭킹 변수
    public Text[] rankers = new Text[5];
    public Text[] rankerTimes = new Text[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SortAndSave()
    {
        //sort
        Debug.Log("정렬시작");

        if (PlayerPrefs.HasKey("Map" + mapNum + "ClearManFirst") && PlayerPrefs.HasKey("Map" + mapNum + "ClearTimeFirst"))
            rankDict.Add(PlayerPrefs.GetString("Map" + mapNum + "ClearManFirst"), PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeFirst"));
        else
            rankDict.Add("null1", 359999);

        if (PlayerPrefs.HasKey("Map" + mapNum + "ClearManSecond") && PlayerPrefs.HasKey("Map" + mapNum + "ClearTimeSecond"))
            rankDict.Add(PlayerPrefs.GetString("Map" + mapNum + "ClearManSecond"), PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeSecond"));
        else
            rankDict.Add("null2", 359999);

        if (PlayerPrefs.HasKey("Map" + mapNum + "ClearManThird") && PlayerPrefs.HasKey("Map" + mapNum + "ClearTimeThird"))
            rankDict.Add(PlayerPrefs.GetString("Map" + mapNum + "ClearManThird"), PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeThird"));
        else
            rankDict.Add("null3", 359999);

        if (PlayerPrefs.HasKey("Map" + mapNum + "ClearManFourth") && PlayerPrefs.HasKey("Map" + mapNum + "ClearTimeFourth"))
            rankDict.Add(PlayerPrefs.GetString("Map" + mapNum + "ClearManFourth"), PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeFourth"));
        else
            rankDict.Add("null4", 359999);

        if (PlayerPrefs.HasKey("Map" + mapNum + "ClearManFifth") && PlayerPrefs.HasKey("Map" + mapNum + "ClearTimeFifth"))
            rankDict.Add(PlayerPrefs.GetString("Map" + mapNum + "ClearManFifth"), PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeFifth"));
        else
            rankDict.Add("null5", 359999);

        if (rankDict.ContainsKey(gmr.inputName.text))
        {
            if (rankDict[gmr.inputName.text] > gmr.GetTime())
                rankDict[gmr.inputName.text] = gmr.GetTime();
        }
        else
            rankDict.Add(gmr.inputName.text, gmr.GetTime());


        List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(rankDict);

        list.Sort((x, y) => x.Value.CompareTo(y.Value));

        //save
        PlayerPrefs.SetString("Map" + mapNum + "ClearManFirst", list[0].Key);
        PlayerPrefs.SetInt("Map" + mapNum + "ClearTimeFirst", list[0].Value);

        PlayerPrefs.SetString("Map" + mapNum + "ClearManSecond", list[1].Key);
        PlayerPrefs.SetInt("Map" + mapNum + "ClearTimeSecond", list[1].Value);

        PlayerPrefs.SetString("Map" + mapNum + "ClearManThird", list[2].Key);
        PlayerPrefs.SetInt("Map" + mapNum + "ClearTimeThird", list[2].Value);

        PlayerPrefs.SetString("Map" + mapNum + "ClearManFourth", list[3].Key);
        PlayerPrefs.SetInt("Map" + mapNum + "ClearTimeFourth", list[3].Value);

        PlayerPrefs.SetString("Map" + mapNum + "ClearManFifth", list[4].Key);
        PlayerPrefs.SetInt("Map" + mapNum + "ClearTimeFifth", list[4].Value);


        Debug.Log("정렬세이브 성공");
    }


    public virtual void Load()
    {
        //1등
        rankers[0].text = PlayerPrefs.GetString("Map" + mapNum + "ClearManFirst");
        times[0] = PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeFirst");
        rankerTimes[0].text = "" + string.Format("{0:00}:{1:00}:{2:00}", times[0] / 3600, (times[0] / 60) % 60, times[0] % 60);

        //2등
        rankers[1].text = PlayerPrefs.GetString("Map" + mapNum + "ClearManSecond");
        times[1] = PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeSecond");
        rankerTimes[1].text = "" + string.Format("{0:00}:{1:00}:{2:00}", times[1] / 3600, (times[1] / 60) % 60, times[1] % 60);

        //3등
        rankers[2].text = PlayerPrefs.GetString("Map" + mapNum + "ClearManThird");
        times[2] = PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeThird");
        rankerTimes[2].text = "" + string.Format("{0:00}:{1:00}:{2:00}", times[2] / 3600, (times[2] / 60) % 60, times[2] % 60);

        //4등
        rankers[3].text = PlayerPrefs.GetString("Map" + mapNum + "ClearManFourth");
        times[3] = PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeFourth");
        rankerTimes[3].text = "" + string.Format("{0:00}:{1:00}:{2:00}", times[3] / 3600, (times[3] / 60) % 60, times[3] % 60);

        //5등
        rankers[4].text = PlayerPrefs.GetString("Map" + mapNum + "ClearManFifth");
        times[4] = PlayerPrefs.GetInt("Map" + mapNum + "ClearTimeFifth");
        rankerTimes[4].text = "" + string.Format("{0:00}:{1:00}:{2:00}", times[4] / 3600, (times[4] / 60) % 60, times[4] % 60);

        Debug.Log("로드 성공");
    }

    public virtual void mapNumChange()
    {

    }

    
}
