using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setup : MonoBehaviour {
    public BGM_Volume_Change bgm_volume_change;
	Button Back_button;
	// Use this for initialization
	void Start () {
		new GameObject( ).AddComponent< SceneNavigator >( );
		Back_button = GameObject.Find ("BackButton").GetComponent< Button > ( );

		Back_button.Select( );
	}
	
	// Update is called once per frame
	void Update ( ) {
		
	}
	public void SceneLoad( ){
        SceneNavigator.Instance.Change( "TGSverTitle", 1.5f );
	}

	public void OnStartButtonClicked1( ){
        Debug.Log ("BGM1");
	}
	public void OnStartButtonClicked2( ){
		Debug.Log ("BGM2");
    }
	public void OnStartButtonClicked3( ){
		Debug.Log ("BGM3");
	}
	public void OnStartButtonClicked4( ){
		Debug.Log ("BGM4");
	}
	public void OnStartButtonClicked5( ){
		Debug.Log ("BGM5");
	}
	public void OnStartButtonClicked6( ){
		Debug.Log ("SE1");
	}
	public void OnStartButtonClicked7( ){
		Debug.Log ("SE2");
	}
	public void OnStartButtonClicked8( ){
		Debug.Log ("SE3");
	}
	public void OnStartButtonClicked9( ){
		Debug.Log ("SE4");
	}
	public void OnStartButtonClicked10( ){
		Debug.Log ("SE5");
	}
}
