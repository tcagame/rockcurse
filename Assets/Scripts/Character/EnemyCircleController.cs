﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleController : MonoBehaviour {

	Rigidbody2D rb2d;

	CircleUpsideControl upsidectrl;
	CircleFrontControl frontctrl;

	const float SPEED = 0.01f;

	Vector3 move_vec;
	Vector3 look_vec;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		upsidectrl = transform.Find("Upside").gameObject.GetComponent< CircleUpsideControl >( );
		frontctrl = transform.Find("Front").gameObject.GetComponent< CircleFrontControl >( );
	}

	void Start ( ) {
		move_vec = Vector3.left;
	}
	
	// Update is called once per frame
	void Update ( ) {
		setLookVec( );
		ActionUpdate( );
	}

	void setLookVec( ) {
		if ( move_vec == Vector3.left ) {
			look_vec = Vector3.forward;
		} else {
			look_vec = Vector3.back;
		}
	}

	void ActionUpdate( ) {
		transform.position += move_vec * SPEED;
		transform.LookAt( transform.position + look_vec );

		if ( upsidectrl._dead ) {
			dead( );
		}
	}

	void jump( ) {
		rb2d.AddForce( new Vector3( 100.0f, 1500.0f, 0 ) );
	}

	void dead( ) {
		Destroy( gameObject );
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Block" && frontctrl._jump ||
			other.gameObject.tag == "Switch" && frontctrl._jump ||
			other.gameObject.tag == "Rock" && frontctrl._jump ) {
			jump( );
		}

		if ( other.gameObject.tag == "Block" ) {
			if ( move_vec == Vector3.left ) {
				move_vec = Vector3.right;
			} else {
				move_vec = Vector3.left;
			}
		}
	}
}
