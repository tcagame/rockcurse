using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Volume_Change : MonoBehaviour {
    // Use this for initialization
    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;
  
    public float masterVolume
    {
        set { mixer.SetFloat("BGM", Mathf.Lerp(-80, 0, value)); }
    }
    
}
