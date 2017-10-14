using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pauser : MonoBehaviour {

	GameObject image;
	Button Title_button;

	public bool _pause;

	// Use this for initialization
	void Start ( ) {
		
		_pause = false;
		image = GameObject.Find("PauseImage").gameObject;
		image.gameObject.SetActive( _pause );

		Title_button = GameObject.Find ("Button").GetComponent< Button > ( );

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
		SceneNavigator.Instance.Change( "TGSverTitle", 1.5f );
	}
}
