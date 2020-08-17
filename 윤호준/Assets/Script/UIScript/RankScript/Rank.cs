using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RankButton()
    {
        if (StageSelect.isRank == false)
        {
            StageSelect.isRank = true;
            GetComponent<Image>().color = new Color(248f/255,129f/255,129f/255);
        }
        else
        {
            StageSelect.isRank = false;
            GetComponent<Image>().color = new Color(1,1,1);
        }
    }

    public virtual void SortAndSave()
    {

    }

    public virtual void Load()
    {

    }
}
