using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockUpsideControl : MonoBehaviour {

	PlayerController pCtrl;

	// Use this for initialization
	void Start () {
		GameObject _player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		pCtrl = _player.GetComponent< PlayerController >( );
	}

	void OnTriggerEnter2D( Collider2D other ){
		if ( other.gameObject.tag == "Player" ) {
			pCtrl.RelayOnTrigger( other, 1 );
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
