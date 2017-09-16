using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLeftsideControl : MonoBehaviour {

	GameObject _player;
	PlayerController pCtrl;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		pCtrl = _player.GetComponent< PlayerController >( );
	}

	void OnTriggerStay2D( Collider2D other ){
		if ( other.gameObject.tag == "Player" ) {
			pCtrl.RelayOnTrigger( other, 2 );
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
