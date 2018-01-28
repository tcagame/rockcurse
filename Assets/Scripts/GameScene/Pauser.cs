using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pauser : MonoBehaviour {

	GameObject image;
	Button Title_button;
	GameObject Audio;

	public bool _pause;
	public bool _return_title;

	// Use this for initialization
	void Start ( ) {
		_pause = false;
		_return_title = false;
		Audio = GameObject.Find( "Audio" );
		image = GameObject.Find("PauseImage").gameObject;
		Title_button = GameObject.Find ("Button").GetComponent< Button > ( );

		image.gameObject.SetActive( _pause );

		Title_button.Select( );
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

	public void SceneLoad( ){
		AudioControl se = Audio.GetComponent<AudioControl>();
		se.Playse( "決定" );
		SceneNavigator.Instance.Change( "TGSverTitle", 1.5f );
		_return_title = true;
	}
}