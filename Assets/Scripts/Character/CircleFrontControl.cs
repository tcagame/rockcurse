using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFrontControl : MonoBehaviour {

	public bool _jump;

	// Use this for initialization
	void Start () {
		_jump = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D( Collider2D other ) {
		_jump = true;
	}

	void OnTriggerExit2D( Collider2D other ) {
		_jump = false;
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Floor" ||
			other.gameObject.tag == "Switch" ||
			other.gameObject.tag == "Rock" ) {
			_jump = false;
		}
	}
}
