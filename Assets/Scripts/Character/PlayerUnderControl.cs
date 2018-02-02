using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnderControl : MonoBehaviour {

	GameObject player;
	PlayerController playerctrl;

	void Awake( ) {
		player = gameObject.transform.parent.gameObject;
		playerctrl = player.GetComponent< PlayerController >( );
	}
	
	void Update( ) {
		
	}

	void OnTriggerStay2D( Collider2D other ) {
		playerctrl.relayOnTriggerStay2D( );
	}

	void OnTriggerExit2D( Collider2D other ) {
		playerctrl.relayOnTriggerExit2D( );
	}
}
