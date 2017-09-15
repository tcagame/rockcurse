using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanksControl : MonoBehaviour {

	void Awake( ) {
		new GameObject( ).AddComponent< SceneNavigator >( );
	}

	void Start( ) {
		
	}
	
	// Update is called once per frame
	void Update( ) {
		inputUpdate( );
	}

	private void inputUpdate( ) {
		if ( Input.GetButtonDown("A") ||
			Input.GetButtonDown("B") ||
			Input.GetButtonDown("X") ||
			Input.GetButtonDown("Y") ) {
			SceneNavigator.Instance.Change( "tgsVerTitle", 2.5f );
		}
	}
}
