using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	
	Button game_main;
    
	public GameObject nodata_window;
    public GameObject parentObject;
    GameObject obj;
    GameObject prefab;
	GameObject Audio;

	const float CHANGESCENE_COUNT = 20.0f;
	public bool LoadGames;
	float change_count;

	void Awake( ) {
		Audio = GameObject.Find( "Audio" );
		new GameObject( ).AddComponent< SceneNavigator >( );
		game_main = GameObject.Find ("start").GetComponent< Button > ( );

		game_main.Select( );
		change_count = CHANGESCENE_COUNT;
	}
	
	void Update( ) {
		loadMovieCountdown( );
	}

	private void loadMovieCountdown( ) {
		change_count -= Time.deltaTime;

		if ( change_count < 0 ) {
			SceneNavigator.Instance.Change( "PromoMov", 2.0f );
		}
	}

	public void StartGame( ){
		AudioControl se = Audio.GetComponent<AudioControl>();
		se.Playse( "決定" );
		SceneNavigator.Instance.Change( "map1", 2.5f );
	}

	public void Continue( ){
		AudioControl se = Audio.GetComponent<AudioControl>();
		se.Playse( "決定" );
		//SceneNavigator.Instance.Change( continue_scene, 1.5f );
	}

	public void Setting( ){
		AudioControl se = Audio.GetComponent<AudioControl>();
		se.Playse( "決定" );
		SceneNavigator.Instance.Change( "set", 1.5f );
	}

	public void GameEnd( ){
		AudioControl se = Audio.GetComponent<AudioControl>();
		se.Playse( "決定" );
        Application.Quit( );
	}
}