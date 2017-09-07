using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Volume_Change : MonoBehaviour {

	// Use this for initialization

	[SerializeField]
	UnityEngine.Audio.AudioMixer mixer;

	public float SE
	{
		set{ mixer.SetFloat("SE_Volume", Mathf.Lerp(0, -80, value)); }
	}

}
