using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selectmap : MonoBehaviour {

	Button start_button;
	int select_map = 0;

	void Start () {
		new GameObject( ).AddComponent< SceneNavigator >( );
		start_button = GameObject.Find ("map1").GetComponent<Button>( );

		start_button.Select ( );

		select_map = GameManager.CountSelect ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartButtonClicked1( ){
		Debug.Log( select_map );
		if ( select_map == 0 ) {
			SceneNavigator.Instance.Change ("map1", 2.5f);
		}
	}
		
	public void OnStartButtonClicked2( ){
		if (select_map == 1) {
			SceneNavigator.Instance.Change ("map2", 2.5f);
		}
	}

	public void OnStartButtonClicked3( ){
		if (select_map == 2) {
			SceneNavigator.Instance.Change ("map3", 2.5f);
		}
	}

	public void OnStartButtonClicked4( ){
		if (select_map == 3) {
			SceneNavigator.Instance.Change ("map4", 2.5f);
		}
	}

	public void OnStartButtonClicked5( ){
		if (select_map == 4) {
			SceneNavigator.Instance.Change ("map5", 2.5f);
		}
	}

	public void OnStartButtonClicked6( ){
		if (select_map == 5) {
			SceneNavigator.Instance.Change ("map6", 2.5f);
		}
	}

	//public void OnStartButtonClicked1( ){
	//	SceneNavigator.Instance.Change( "map7", 2.5f );
	//}

	public void OnStartButtonClicked7( ){
		SceneNavigator.Instance.Change( "TGSverTitle", 2.5f );
	}
}
