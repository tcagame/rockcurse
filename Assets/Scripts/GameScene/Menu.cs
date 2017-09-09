using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject nodata_window;
    public GameObject parentObject;
    GameObject obj;
    GameObject prefab;
	public bool LoadGames;

	Button game_main;

	//Selectable sel;

	// Use this for initialization
	void Start () {
		//sel = GetComponent<Selectable> ();
		game_main = GameObject.Find ("start").GetComponent< Button > ( );

		game_main.Select( ); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartButtonClicked1( ){
		FadeManager.Instance.LoadScene( "map1", 3.0f );
	}

	public void OnStartButtonClicked2(){
		if (!LoadGames) {
			Debug.Log( "file not found" );
            obj = (GameObject)Resources.Load( "Prefab/NoData" );
            prefab = (GameObject)Instantiate(obj);
            prefab.transform.SetParent(parentObject.transform, false);
            Destroy(prefab, 2.0f);
			//FadeManager.Instance.LoadScene( "main", 3.0f );
		} else {
			FadeManager.Instance.LoadScene( "continue", 1.5f );
		}
	}

	public void OnStartButtonClicked3(){
		FadeManager.Instance.LoadScene( "set", 1.5f );
	}

	/*
	public void GameEnd( ){
		UnityEditor.EditorApplication.isPlaying = false;
	}
	*/
}