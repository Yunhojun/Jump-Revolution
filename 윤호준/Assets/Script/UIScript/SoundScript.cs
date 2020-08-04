using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static SoundScript Inst;

    AudioSource myaudio;

    public AudioClip jump;                  

    public AudioClip shot;
    public AudioClip item;
    public AudioClip getShoted;

    // Start is called before the first frame update

    void Start()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        myaudio = gameObject.AddComponent<AudioSource>(); // 오디오 만들기

    }

    public void jumpPlayer() // 외부에서도 써야하기 때문에 public 으로 
    {
        myaudio.PlayOneShot(jump);  // playoneshot은 파라미터 안에 넣은 소리를 한번 재생시켜준다.
    }

    public void shotPlayer()
    {
        myaudio.volume = 1.0f;   // 오디오 볼륨을 줄여준다. (0.0f ~1.0f)
        myaudio.PlayOneShot(shot);
    }
    public void itemPlayer()
    {
        myaudio.volume = 1.0f;   // 오디오 볼륨을 줄여준다. (0.0f ~1.0f)
        myaudio.PlayOneShot(item);
    }
    public void getShotedPlayer()
    {
        myaudio.volume = 1.0f;   // 오디오 볼륨을 줄여준다. (0.0f ~1.0f)
        myaudio.PlayOneShot(getShoted);
    }
}