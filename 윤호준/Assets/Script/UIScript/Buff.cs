using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public static Buff buffInstance;

    public GameObject[] buff = new GameObject[2];
    public Image[] buffIcon = new Image[2];

    private bool firstBuffOn = false;
    private bool secondBuffOn = false;

    private Transform[] buffTrf = new Transform[2];
    Vector2[] originalPos = new Vector2[2];
    bool[] buffType = new bool[2];
    Coroutine[] co = new Coroutine[2];

    private void Start()
    {
        if(buffInstance == null)
        {
            buffInstance = this;
        }

        buffTrf[0] = buff[0].transform;
        buffTrf[1] = buff[1].transform;
        originalPos[0] = buffTrf[0].localPosition;
        originalPos[1] = buffTrf[1].localPosition;
    }

    public void BuffStrart(Sprite s, float duration, bool type)
    {
        if(!firstBuffOn && !secondBuffOn)
        {
            buffIcon[0].sprite = s;
            buffType[0] = type;
            co[0] = StartCoroutine(FirstBuffCount(duration));
        }
        else if (firstBuffOn && !secondBuffOn)
        {
            if(buffType[0] == type)
            {
                buffIcon[0].sprite = s;
                StopCoroutine(co[0]);
                co[0] = StartCoroutine(FirstBuffCount(duration));
            }
            else
            {
                buffIcon[1].sprite = s;
                buffType[1] = type;
                co[1] = StartCoroutine(SecondBuffCount(duration));
            }
        }
        else if(!firstBuffOn && secondBuffOn)
        {
            if (buffType[1] == type)
            {
                buffIcon[1].sprite = s;
                StopCoroutine(co[1]);
                co[1] = StartCoroutine(SecondBuffCount(duration));
            }
            else
            {
                buffIcon[0].sprite = s;
                buffType[0] = type;
                co[0] = StartCoroutine(FirstBuffCount(duration));
            }
        }
        else if(firstBuffOn && secondBuffOn)
        {
            if(buffType[0] == type)
            {
                buffIcon[0].sprite = s;
                StopCoroutine(co[0]);
                co[0] = StartCoroutine(FirstBuffCount(duration));
            }
            else if(buffType[1] == type)
            {
                buffIcon[1].sprite = s;
                StopCoroutine(co[1]);
                co[1] = StartCoroutine(SecondBuffCount(duration));
            }
        }
    }

    void InitializePos()
    {
        buffTrf[0].localPosition = originalPos[0];
        buffTrf[1].localPosition = originalPos[1];
    }

    IEnumerator FirstBuffCount(float duration)
    {
        firstBuffOn = true;
        buff[0].SetActive(true);
        if (secondBuffOn)
        {
            if((Vector2)buffTrf[1].localPosition == originalPos[0])
                buffTrf[0].localPosition = originalPos[1];
            else
                buffTrf[0].localPosition = originalPos[0];
        }
        else
        {
            buffTrf[0].localPosition = originalPos[0];
        }

        print("111");
        float buffTime = 0f;
        while (buffTime < duration)
        {
            buffIcon[0].fillAmount = 1f - (buffTime / duration);

            yield return new WaitForFixedUpdate();

            buffTime += Time.deltaTime;
        }

        buff[0].SetActive(false);
        firstBuffOn = false;

        if (secondBuffOn)
        {
            buffTrf[1].localPosition = originalPos[0];
        }
        else
        {
            InitializePos();
        }
    }

    IEnumerator SecondBuffCount(float duration)
    {
        secondBuffOn = true;
        buff[1].SetActive(true);
        print("222");
        if (firstBuffOn)
        {
            if ((Vector2)buffTrf[0].localPosition == originalPos[0])
                buffTrf[1].localPosition = originalPos[1];
            else
                buffTrf[1].localPosition = originalPos[0];
        }
        else
        {
            buffTrf[1].localPosition = originalPos[0];
        }

        float buffTime = 0f;
        while (buffTime < duration)
        {
            buffIcon[1].fillAmount = 1f - (buffTime / duration);

            yield return new WaitForFixedUpdate();

            buffTime += Time.deltaTime;
        }

        buff[1].SetActive(false);
        secondBuffOn = false;

        if (!firstBuffOn)
        {
            InitializePos();
        }
        else if(firstBuffOn && originalPos[0] == (Vector2)buffTrf[1].localPosition)
        {
            buffTrf[0].localPosition = originalPos[0];
        }
    }
}
