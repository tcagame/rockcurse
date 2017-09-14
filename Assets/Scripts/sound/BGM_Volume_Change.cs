using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Volume_Change : MonoBehaviour {
    // Use this for initialization
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() {
        
    }
    public void OnStartButtonClicked1()
    {
        audioSource.volume = 0.2f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClicked2()
    {
        audioSource.volume = 0.4f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClicked3()
    {
        audioSource.volume = 0.6f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClicked4()
    {
        audioSource.volume = 0.8f;
        Debug.Log(audioSource.volume);
    }
    public void OnStartButtonClicked5()
    {
        audioSource.volume = 1f;
        Debug.Log(audioSource.volume);
    }
}
