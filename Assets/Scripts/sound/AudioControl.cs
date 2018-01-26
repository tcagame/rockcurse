using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {

    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;

    public float masterVolumeBGM
    {
        set { mixer.SetFloat("BGM", Mathf.Lerp(-80, 0, value)); }
    }

    public float masterVolumeSE
    {
        set { mixer.SetFloat("SE", Mathf.Lerp(-80, 0, value)); }
    }

    public AudioSource BGM,SE;
    private string _nextBGMName, _nextSEName;
    private Dictionary<string, AudioClip> _bgmDic, _seDic;

    private void Awake() {
        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();
        object[] bgmList = Resources.LoadAll("Sound/music_true/BGM");
        object[] seList = Resources.LoadAll("Sound/music_true/SE");

        foreach (AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }
        foreach (AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }
    }

    void Start() {
        BGM.Play();
    }

    public void GameoverBGM() {
        BGM.Stop();
    }

    /// <summary>
    /// 指定したファイル名のBGMを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    public void Playbgm(string _bgmName, float delay = 0.0f)
    {
        _nextBGMName = _bgmName;
        Invoke("DelayPlayBGM", delay);
    }

    private void DelayPlayBGM()
    {
        BGM.PlayOneShot(_bgmDic[_nextBGMName] as AudioClip);
    }

    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    public void Playse(string _seName, float delay = 0.0f) {
        _nextSEName = _seName;
        Invoke("DelayPlaySE", delay);
    }

    private void DelayPlaySE() {
        SE.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }
}
