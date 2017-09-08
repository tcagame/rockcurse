using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	Rigidbody2D rb2d;

	Vector3 move_vec;
	const float SPEED = 40.0f;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
	}

	void Start ( ) {
		move_vec = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
		ActionUpdate( );
	}

	void ActionUpdate( ) {
		rb2d.AddForce( move_vec * SPEED );
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.transform.position.y > transform.position.y + 2.8f ) {
			if (other.gameObject.tag == "Rock") {
				Destroy (gameObject);
			}
		}
	}
}
