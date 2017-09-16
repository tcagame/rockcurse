using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

	public Image image;

	// Use this for initialization
	void Start () {
		image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKey(KeyCode.A) ) {
			image.enabled = true;
		}
		if( !Input.GetKey(KeyCode.A) ) {
			image.enabled = false;
		}
		/*if( Input.GetButton( "LB" ) ) {
			image.enabled = true;
		}
		if( !Input.GetButton( "LB" ) ) {
			image.enabled = false;
		}*/
	}
}
