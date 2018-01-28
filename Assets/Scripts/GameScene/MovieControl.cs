using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieControl : MonoBehaviour {

	Renderer rend;
	MovieTexture movtex;
	AudioSource audiosource;

	const float STOP_TIME = 1.5f;
	float stop_time;
	bool ischanging;

	// Use this for initialization
	void Start( ) {
		new GameObject( ).AddComponent< SceneNavigator >( );

		audiosource = gameObject.GetComponent< AudioSource >( );
		rend = GetComponent< Renderer >( );

		stop_time = STOP_TIME;
		ischanging = false;

		movtex = (MovieTexture)rend.material.mainTexture;
		movtex.loop = false;
		audiosource.Play( );
		movtex.Play( );
	}
	
	void Update ( ) {
		if ( !movtex.isPlaying ) {
			SceneNavigator.Instance.Change( "Title", 1.0f );
			ischanging = true;
		}

		if ( Input.GetButtonDown("A") || 
			Input.GetButtonDown("B") || 
			Input.GetButtonDown("X") || 
			Input.GetButtonDown("Y") ) {
			SceneNavigator.Instance.Change( "Title", 2.0f );
			ischanging = true;
		}

		if ( ischanging ) {
			stop_time -= Time.deltaTime;
			if ( stop_time < 0 ) {
				audiosource.Stop( );
				movtex.Stop( );
			}
		}
	}
}
