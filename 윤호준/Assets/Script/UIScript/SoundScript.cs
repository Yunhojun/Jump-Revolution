using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    public static SoundScript Inst;
    public Slider backVolume;
    private float backVol = 1f;
    public AudioSource myaudio;

    public AudioClip jump;                  

    public AudioClip shot;
    public AudioClip item;
    public AudioClip explosion;

    // Start is called before the first frame update

    private void Start()
    {
        if (Inst == null)
        {
            Inst = this;
        }

        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        myaudio.volume = backVolume.value;
    }

    void Update()
    {
        SoundSlider();
    }
    public void SoundSlider()
    {
        myaudio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }

    public void jumpPlayer() // 외부에서도 써야하기 때문에 public 으로 
    {
        myaudio.PlayOneShot(jump);  // playoneshot은 파라미터 안에 넣은 소리를 한번 재생시켜준다.
    }

    public void shotPlayer()
    {  
        myaudio.PlayOneShot(shot);
    }
    public void itemPlayer()
    {
        myaudio.PlayOneShot(item);
    }
    public void explosionPlayer()
    {
        myaudio.PlayOneShot(explosion);
    }
}