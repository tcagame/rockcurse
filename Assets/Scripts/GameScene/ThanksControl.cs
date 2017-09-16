using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanksControl : MonoBehaviour {

	SpriteRenderer sr;

	Color color;

	const float FADE_TIME = 0.0f;

	float alpha;
	float fade_time;
	bool inputcut;

	void Awake( ) {
		sr = GetComponent< SpriteRenderer >( );
		new GameObject( ).AddComponent< SceneNavigator >( );
	}

	void Start( ) {
		alpha = 0;
		fade_time = FADE_TIME;
		inputcut = true;
	}
	
	// Update is called once per frame
	void Update( ) {
		fadeinUpdate( );
		if ( !inputcut ) {
			inputUpdate( );
		}
	}

	private void fadeinUpdate( ) {
		fade_time += Time.deltaTime;

		if ( fade_time < FADE_TIME ) {
			alpha = fade_time / FADE_TIME;
			color = sr.color;
			color.a = alpha;
			sr.color = color;
		}
		if ( fade_time > FADE_TIME ) {
			inputcut = false;
		}
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
