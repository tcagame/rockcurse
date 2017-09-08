using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	GameObject canvas;

	bool _pause;

	// Use this for initialization
	void Start ( ) {
		_pause = false;
		canvas = GameObject.Find("Canvas").gameObject;
		canvas.gameObject.SetActive( _pause );
	}

	// Update is called once per frame
	void Update ( ) {
		distinction( );
	}

	void distinction( ) {
		if ( Application.loadedLevelName == "title" ) {
			titleUpdate( );
		}
		if ( Application.loadedLevelName == "main" ) {
			mainUpdate( );
		}
		if ( Application.loadedLevelName == "set" ) {
			setUpdate( );
		}
		if ( Application.loadedLevelName == "GameOver" ) {
			gameoverUpdate( );
		}
	}

	void titleUpdate( ) {

	}

	void mainUpdate( ) {
		if ( Input.GetButtonDown("Start") ) {
			if ( _pause ){
				_pause = false;
				Time.timeScale = 1;
			} else {
				_pause = true;
				Time.timeScale = 0;
			}
			canvas.gameObject.SetActive( _pause );
		}
	}

	void setUpdate( ) {

	}

	void gameoverUpdate( ) {

	}
}
