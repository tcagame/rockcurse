using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pauser : MonoBehaviour {

	GameObject image;
	Button Title_button;
	GameObject Audio;

	public bool _pause;

	void Awake( ) {
		Audio = GameObject.Find( "Audio" );
		image = GameObject.Find("PauseImage").gameObject;
		Title_button = GameObject.Find("Button").GetComponent< Button > ( );
	}

	void Start ( ) {
		_pause = false;

		image.gameObject.SetActive( _pause );
		Title_button.Select( );
	}

	void Update ( ) {
		Pausing( );
	}

	private void Pausing( ) {
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

	private void SceneLoad( ){
		AudioControl se = Audio.GetComponent< AudioControl >( );
		se.Playse( "決定" );
		Time.timeScale = 1;
		SceneNavigator.Instance.Change( "Title", 1.5f );
	}
}