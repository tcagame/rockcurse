using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Volume_Change : MonoBehaviour {
    // Use this for initialization
    AudioSource audioSource;

    void Start()
    {
        //audioSourceの値を取得
        audioSource = GetComponent<AudioSource>();
    }
    public void OnStartButtonClickedBGM1()
    {
        audioSource.volume = 0.2f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedBGM2()
    {
        audioSource.volume = 0.4f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedBGM3()
    {
        audioSource.volume = 0.6f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedBGM4()
    {
        audioSource.volume = 0.8f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedBGM5()
    {
        audioSource.volume = 1f;
        Debug.Log(audioSource.volume);
    }

    private class SaveSound
    {
        public static float value_bgm { get; internal set; }
    }
}
