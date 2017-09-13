using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	Rigidbody2D rb2d;

	const float SPEED = 1.0f;
	const float FLAP = 250.0f;
	const float MAX_VEL_X = 1.5f;

	Vector3 move_vec;

	bool jump;
	float height;
	float target_height;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		height = gameObject.GetComponent< SpriteRenderer >( ).bounds.size.y;
	}

	void Start ( ) {
		jump = false;
		move_vec = Vector3.right;
		target_height = 0;
	}
	
	// Update is called once per frame
	void Update ( ) {
		ActionUpdate( );
	}

	void FixedUpdate( ) {
		// 移動速度制限処理
		Vector2 vel = rb2d.velocity;
		vel.y = 0f;
		if ( vel.magnitude > MAX_VEL_X ) {
			vel =  rb2d.velocity - ( vel.normalized * MAX_VEL_X );
			vel.y = 0f;
			rb2d.velocity -= vel;
		}
	}

	void ActionUpdate( ) {
		rb2d.AddForce( move_vec * SPEED );
		transform.LookAt( transform.position + move_vec );

		if ( jump ) {
			rb2d.AddForce( Vector2.up * FLAP );
		}
	}

	void OnCollisionEnter2D( Collision2D other ) {
		target_height = other.gameObject.GetComponent< SpriteRenderer >( ).bounds.size.y;

		if (height * 0.4f > target_height) {
			jump = true;
		} else {
			if ( move_vec == Vector3.right ) {
				move_vec = Vector3.left;
			}
			if ( move_vec == Vector3.left ) {
				move_vec = Vector3.right;
			}
		}

		if ( other.gameObject.transform.position.y > transform.position.y + 1.0f ) {
			if ( other.gameObject.tag == "Rock" ) {
				Destroy ( gameObject );
			}
		}

		if ( other.gameObject.tag == "Floor" || other.gameObject.tag == "Rock" ) {
			jump = false;
		}
	}

}
