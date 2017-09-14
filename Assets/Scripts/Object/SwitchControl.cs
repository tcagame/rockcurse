using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {

	const float SPEED = -0.05f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Rock" ) {
			Press( );
		}
	}

	void Press( ) {
		if ( transform.position.y > -7.95f ) {
			transform.position = transform.position + new Vector3( 0, SPEED, 0 );
		} else {
			Destroy( gameObject );
		}
	}
}
