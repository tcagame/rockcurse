using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {
	Button continue_game;

	// Use this for initialization
	void Start () {
		continue_game = GameObject.Find ("Continue_1").GetComponent<Button> ();

		continue_game.Select (); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnStartButtonClicked1( ){
		FadeManager.Instance.LoadScene( "main", 1.5f );
	}

	public void OnStartButtonClicked2( ){
		UnityEditor.EditorApplication.isPlaying = false;
	}
}
