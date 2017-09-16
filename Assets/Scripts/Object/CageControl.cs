using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageControl : MonoBehaviour {

	GameObject switcher;

	const float SPEED = -0.1f;

	// Use this for initialization
	void Start ( ) {
		switcher = GameObject.Find("Switch").gameObject;
	}
	
	// Update is called once per frame
	void Update ( ) {
		searchMove( );
	}

	void searchMove( ) {
		if ( switcher == null ) {
			if ( transform.position.y > -10.0f ) {
				transform.position = transform.position + new Vector3( 0, SPEED, 0 );
			} else {
				Destroy( gameObject );
			}
		}
	}
}
