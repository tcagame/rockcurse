using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Volume_Change : MonoBehaviour {

    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;

    public float masterVolume
    {
        set { mixer.SetFloat("SE", Mathf.Lerp(-80, 0, value)); }
    }

}
