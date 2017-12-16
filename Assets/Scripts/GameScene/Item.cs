using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	
	SpriteRenderer MainSpriteRenderer;
	public Sprite _itemCube;
	public Sprite _itemCircle;

	// Use this for initialization
	void Start () {
		MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		SelectItem ();
	}

	void SelectItem( ){
		if ( Input.GetKey( KeyCode.J ) ) {
			MainSpriteRenderer.sprite = _itemCube;
		} 
		if ( Input.GetKey( KeyCode.K ) ) {
			MainSpriteRenderer.sprite = _itemCircle;
		}
	}
}
//lj rk