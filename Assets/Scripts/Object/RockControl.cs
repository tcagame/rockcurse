﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControl : MonoBehaviour {

	float time = 8.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;

		if( time < 0 ) {
			Destroy ( this.gameObject );
		}
	}
}
