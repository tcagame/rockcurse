using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour {

	GameObject image;

	public bool _pause;

	// Use this for initialization
	void Start ( ) {
		_pause = false;
		image = GameObject.Find("PauseImage").gameObject;
		image.gameObject.SetActive( _pause );
	}

	// Update is called once per frame
	void Update ( ) {
		Pausing( );
	}

	void Pausing( ) {
		if ( Input.GetButtonDown("Start") ) {
			if ( _pause ){
				_pause = false;
				Time.timeScale = 1;
			} else {
				_pause = true;
				Time.timeScale = 0;
			}
			image.gameObject.SetActive( _pause );
		}
	}
}
