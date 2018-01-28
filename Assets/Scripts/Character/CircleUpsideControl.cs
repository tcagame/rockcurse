using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleUpsideControl : MonoBehaviour {

	public bool _dead;

	// Use this for initialization
	void Start () {
		_dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.tag == "Rock" ) {
			_dead = true;
		}
	}
}
