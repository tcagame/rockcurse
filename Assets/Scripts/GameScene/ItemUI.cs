using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
	SpriteRenderer MainSpriteRenderer;

	public Sprite[] _itemUI;
	public GameObject Item_image;
    float item_span;
    float change_span;
    int count;

    bool _item;

    // Use this for initialization
    void Start () {
		MainSpriteRenderer = Item_image.GetComponent<SpriteRenderer> ();
		MainSpriteRenderer.sprite = _itemUI[2];
		Item_image.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
        SetItem();
        item_span -= Time.deltaTime;
        change_span -= Time.deltaTime;
        if (item_span <= 0) {
            NotActiveItem();
		} 
        if (change_span <= 0) {
            SelectItem();
        }
	}
	//非表示
	void NotActiveItem( ){
		Item_image.SetActive (false);
		_item = false;
        count = 0;
	}

    void SetItem() {
        if (Input.GetButtonDown("LB") && !_item)
        {
            Item_image.SetActive(true);
            _item = true;
            MainSpriteRenderer.sprite = _itemUI[2];
            item_span = 2.5f;
            change_span = 0.1f;
        }
        if (Input.GetButtonDown("RB") && !_item)
        {
            Item_image.SetActive(true);
            _item = true;
            MainSpriteRenderer.sprite = _itemUI[2];
            item_span = 2.5f;
            change_span = 0.1f;
        }
    }

	void SelectItem( ){

		if (Input.GetButtonDown ("LB")) {
		    MainSpriteRenderer.sprite = _itemUI [0];
            item_span = 2.5f;
        }
		if (Input.GetButtonDown ("RB")) {
			MainSpriteRenderer.sprite = _itemUI [1];
            item_span = 2.5f;
        }
	}

	//KeyCode"J""K"
	//KeyCodeは仮で重複
}
