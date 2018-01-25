using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {
    GameObject Audio;
    const float SPEED = -0.05f;

	// Use this for initialization
	void Start () {
        Audio = GameObject.Find("Audio");
    }

    // Update is called once per frame
    void Update () {
        
	}

	void OnCollisionEnter2D( Collision2D other ) {
        if ( other.gameObject.tag == "Rock" ) {
            Press( );
		}
	}

	void Press( ) {
        AudioControl se1 = Audio.GetComponent<AudioControl>();
        se1.Playse("スイッチ");
        if ( transform.position.y > -7.85f ) {
            transform.position = transform.position + new Vector3( 0, SPEED, 0 );
        } else {
            Destroy( gameObject );
            se1.Playse("門が開く");
        }
	}
}
