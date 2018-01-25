using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {

    public AudioSource BGM,SE;
    private string _nextSEName;
    private Dictionary<string, AudioClip> _seDic;

    private void Awake() {
        _seDic = new Dictionary<string, AudioClip>();
        object[] seList = Resources.LoadAll("Sound/music_true/SE");

        foreach (AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }
    }

    public void Playbgm() {
        BGM.Play();
    }

    public void Playse(string _seName, float delay = 0.0f) {
        _nextSEName = _seName;
        Invoke("DelayPlaySE", delay);
    }

    private void DelayPlaySE() {
        SE.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }
}
