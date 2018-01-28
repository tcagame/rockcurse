using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanksControl : MonoBehaviour {

	const float EXIT_TIME = 10.0f;

	float exit_time;

	void Awake( ) {
		new GameObject( ).AddComponent< SceneNavigator >( );
	}

	void Start( ) {
		exit_time = EXIT_TIME;
	}
	
	// Update is called once per frame
	void Update( ) {
		inputUpdate( );
	}

	private void inputUpdate( ) {
		exit_time -= Time.deltaTime;

		if ( Input.GetButtonDown("A") ||
			Input.GetButtonDown("B") ||
			Input.GetButtonDown("X") ||
			Input.GetButtonDown("Y") ||
			exit_time < 0 ) {
			SceneNavigator.Instance.Change( "Title", 2.5f );
		}
	}
}
