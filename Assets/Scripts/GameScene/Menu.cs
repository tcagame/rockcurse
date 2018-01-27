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

	// Use this for initialization
	void Start( ) {
		Audio = GameObject.Find( "Audio" );
		new GameObject( ).AddComponent< SceneNavigator >( );
		game_main = GameObject.Find ("start").GetComponent< Button > ( );

		game_main.Select( );
		change_count = CHANGESCENE_COUNT;
	}
	
	// Update is called once per frame
	void Update( ) {
		loadMovieCountdown ();
	}

	private void loadMovieCountdown( ) {
		change_count -= Time.deltaTime;

		if ( change_count < 0 ) {
			SceneNavigator.Instance.Change( "PromoMov", 2.0f );
		}
	}

	public void OnStartButtonClicked1( ){
		AudioControl se = Audio.GetComponent<AudioControl>();
		se.Playse( "決定" );
		SceneNavigator.Instance.Change( "map1", 2.5f );
	}

	public void OnStartButtonClicked2( ){
		AudioControl se = Audio.GetComponent<AudioControl>();
		se.Playse( "決定" );
		if ( !LoadGames ) {
			Debug.Log( "file not found" );
            obj = (GameObject)Resources.Load( "Prefab/NoData" );
            prefab = (GameObject)Instantiate( obj );
            prefab.transform.SetParent( parentObject.transform, false );
            Destroy( prefab, 2.0f );
			//FadeManager.Instance.LoadScene( "main", 3.0f );
		} else {
			SceneNavigator.Instance.Change( "SelectMap", 1.5f );
		}
	}

	public void OnStartButtonClicked3( ){
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