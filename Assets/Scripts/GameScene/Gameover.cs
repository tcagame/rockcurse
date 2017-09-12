using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {
	
	Button continue_game;
	GameManager gm;

	// Use this for initialization
	void Start ( ) {
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameManager >( );
		continue_game = GameObject.Find("Continue_1").GetComponent< Button >( );

		continue_game.Select( );
	}
	
	// Update is called once per frame
	void Update ( ) {
		
	}
	public void OnStartButtonClicked1( ) {
		FadeManager.Instance.LoadScene( "map1", 1.5f );
	}

	public void OnStartButtonClicked2( ) {
		FadeManager.Instance.LoadScene( "Title", 1.5f );
	}
}
