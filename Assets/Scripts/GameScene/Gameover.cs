﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {
	
	Button continue_game;
	GameManager gm;


	// Use this for initialization
	void Start ( ) {
		new GameObject( ).AddComponent< SceneNavigator >( );
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameManager >( );
		continue_game = GameObject.Find("retry").GetComponent< Button >( );

		continue_game.Select( );
	}
	
	// Update is called once per frame
	void Update ( ) {
		
	}

	public void OnStartButtonClicked1( ) {
		SceneNavigator.Instance.Change( gm._current_scene, 1.5f );
	}

	public void OnStartButtonClicked2( ) {
		SceneNavigator.Instance.Change( "Title", 1.5f );
	}
}
