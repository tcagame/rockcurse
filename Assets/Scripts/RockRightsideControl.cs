using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRightsideControl : MonoBehaviour {

	PlayerController pCtrl;

	// Use this for initialization
	void Start () {
		GameObject _player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		pCtrl = _player.GetComponent< PlayerController >( );
	}

	void OnTriggerStay2D( Collider2D other ){
		if ( other.gameObject.tag == "Player" ) {
			pCtrl.RelayOnTrigger( other, 3 );
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
