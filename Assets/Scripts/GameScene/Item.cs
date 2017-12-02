using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	GameObject image;
	Button Item_button;

	public bool _item;

	// Use this for initialization
	void Start () {
		_item = false;
		image = GameObject.Find("ItemUI").gameObject;
		image.gameObject.SetActive(_item);

		Item_button = GameObject.Find("Button").GetComponent<Button> ();
		Item_button.Select( );
	}
	
	// Update is called once per frame
	void Update () {
		SelectItem ();
	}

	void SelectItem( ){
		if (Input.GetButtonDown ("Start") ) {
			if (_item) {
				_item = false;
			} else {
				_item = true;
			}
			image.gameObject.SetActive (_item);
		}
	}
}
