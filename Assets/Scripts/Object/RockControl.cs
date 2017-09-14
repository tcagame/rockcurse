﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControl : MonoBehaviour {

	Rigidbody2D rb2d;
	SpriteRenderer sr;
	Animator anim;

	Color color;

	const float GEN_TIME = 1.0f;
	const float LIMIT_TIME = 2.0f;

	float alpha;
	float generate_time;
	float destroy_time;
	bool limit;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		sr = GetComponent< SpriteRenderer >( );
		anim = GetComponent< Animator >( );
	}

	void Start( ) {
		rb2d.isKinematic = true;
		generate_time = 0;
		alpha = 0;

		limit = false;
		destroy_time = 10.0f;
	}
	
	void Update( ) {
		GenerateUpdate( );
		DestroyUpdate( );
		AnimationUpdate( );
	}

	void GenerateUpdate( ) {
		generate_time += Time.deltaTime;

		if ( generate_time < GEN_TIME ) {
			alpha = generate_time / GEN_TIME;
			color = sr.color;
			color.a = alpha;
			sr.color = color;
		} else {
			rb2d.isKinematic = false;
		}
	}

	void DestroyUpdate( ) {
		destroy_time -= Time.deltaTime;

		if ( destroy_time < LIMIT_TIME ) {
			limit = true;
		}
		if( destroy_time < 0 ) {
			Destroy ( this.gameObject );
		}
	}

	void AnimationUpdate( ) {
		anim.SetBool( "Limit", limit );
	}
}
