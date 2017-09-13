using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_vol1 : MonoBehaviour {
    // Use this for initialization

    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;

    float BGM
    {
        set { mixer.SetFloat("BGM_Volume", Mathf.Lerp(-80, value)); }
    }

}
