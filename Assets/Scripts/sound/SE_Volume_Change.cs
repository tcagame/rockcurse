using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Volume_Change : MonoBehaviour {

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnStartButtonClickedSE1()
    {
        audioSource.volume = 0.1f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedSE2()
    {
        audioSource.volume = 0.2f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedSE3()
    {
        audioSource.volume = 0.3f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedSE4()
    {
        audioSource.volume = 0.4f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClickedSE5()
    {
        audioSource.volume = 0.5f;
        Debug.Log(audioSource.volume);
    }
}
