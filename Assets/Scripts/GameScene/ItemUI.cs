using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
	SpriteRenderer MainSpriteRenderer;

	public Sprite[] _itemUI;
	public GameObject Item_image;

	private int i;
	bool _item;
	bool _Cube;
	bool _Circle;



	// Use this for initialization
	void Start () {
		i = 0;
		MainSpriteRenderer = Item_image.GetComponent<SpriteRenderer> ();
		MainSpriteRenderer.sprite = _itemUI[2];
		Item_image.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("B") && !_item ) {
			Item_image.SetActive (true);
			_item = true;
			MainSpriteRenderer.sprite = _itemUI [2];
			Invoke ("NotActiveItem", 3f);
		} 
		SelectItem ();
	}

	void NotActiveItem( ){
		i = 0;
		Item_image.SetActive (false);
		_item = false;
	}

	void SelectItem( ){
		if (Input.GetButtonDown ("X")) {
			MainSpriteRenderer.sprite = _itemUI [0];
		}
		if (Input.GetButtonDown ("A")) {
			MainSpriteRenderer.sprite = _itemUI [1];
		}
	}

	//KeyCode"J""K"
	//KeyCodeは仮で重複
}
