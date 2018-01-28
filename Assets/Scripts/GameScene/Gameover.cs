using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {
	
	Button continue_game;
	GameManager gm;
	AudioControl ad;
    GameObject Audio;

	void Awake( ) {
		Audio = GameObject.Find("Audio");
		new GameObject( ).AddComponent< SceneNavigator >( );
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameManager >( );
		continue_game = GameObject.Find("retry").GetComponent< Button >( );
		ad = Audio.GetComponent< AudioControl >( );

		ad.GameoverBGM( );
		ad.Playse( "gameover" );
		continue_game.Select( );
	}

	void Start ( ) {
		
	}
	
	void Update ( ) {
		
	}

	public void OnStartButtonClicked1( ) {
		AudioControl se = Audio.GetComponent< AudioControl >( );
		se.Playse( "決定" );
        SceneNavigator.Instance.Change( gm._current_scene, 1.5f );
	}

	public void OnStartButtonClicked2( ) {
		AudioControl se = Audio.GetComponent< AudioControl >( );
		se.Playse( "決定" );
		SceneNavigator.Instance.Change( "thanks", 1.5f );
	}
}
