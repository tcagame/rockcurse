using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Volume_Change : MonoBehaviour {
	// Use this for initialization

	[SerializeField]
	UnityEngine.Audio.AudioMixer mixer;

	public float BGM
	{
		set{ mixer.SetFloat("BGM_Volume", Mathf.Lerp(0, -80, value)); }
	}

}
