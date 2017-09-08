using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start ( ) {
		
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
		
	}

	void setUpdate( ) {

	}

	void gameoverUpdate( ) {

	}
}
