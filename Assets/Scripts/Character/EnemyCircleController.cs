using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;
	PolygonCollider2D polycol2d;
	AnimatorStateInfo animstate;
	SpriteRenderer sr;
	Color color;

	CircleUpsideControl upsidectrl;
	CircleFrontControl frontctrl;

	Vector3 move_vec;
	Vector3 look_vec;

	const float SPEED = 0.01f;
	const float FADE_TIME = 1.0f;

	public bool _isdead;

	float duration;
	float anim_nomalized_time;
	float alpha;
	float fade_time;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		anim = GetComponent< Animator >( );
		polycol2d = GetComponent< PolygonCollider2D >( );
		sr = GetComponent< SpriteRenderer >( );
		upsidectrl = transform.Find("Upside").gameObject.GetComponent< CircleUpsideControl >( );
		frontctrl = transform.Find("Front").gameObject.GetComponent< CircleFrontControl >( );
	}

	void Start ( ) {
		move_vec = Vector3.left;
		_isdead = false;
		alpha = 0;
		fade_time = FADE_TIME;
	}
	
	// Update is called once per frame
	void Update ( ) {
		animstate = anim.GetCurrentAnimatorStateInfo(0);
		duration = animstate.length;
		anim_nomalized_time = animstate.normalizedTime;

		setLookVec( );
		ActionUpdate( );
		AnimatorUpdate( );
	}

	private void setLookVec( ) {
		if ( move_vec == Vector3.left ) {
			look_vec = Vector3.forward;
		} else {
			look_vec = Vector3.back;
		}
	}

	void ActionUpdate( ) {
		if ( upsidectrl._dead ) {
			dead( );
		}
		if ( !_isdead ) {
			transform.position += move_vec * SPEED;
			transform.LookAt( transform.position + look_vec );
		}
	}

	private void jump( ) {
		rb2d.AddForce( new Vector3( 100.0f, 1500.0f, 0 ) );
	}

	private void dead( ) {
		_isdead = true;


		if ( _isdead && duration >= 2.5f && anim_nomalized_time >= 0.45f ) {
			fade_time -= Time.deltaTime;

			if ( fade_time > 0 ) {
				alpha = fade_time / FADE_TIME;
				color = sr.color;
				color.a = alpha;
				sr.color = color;
			}
			if ( fade_time < 0 ) {
				Destroy( gameObject );
			}
		}
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Block" && frontctrl._jump ||
			other.gameObject.tag == "Switch" && frontctrl._jump ||
			other.gameObject.tag == "Rock" && frontctrl._jump ) {
			jump( );
		}

		if ( other.gameObject.tag == "Block" ) {
			if ( move_vec == Vector3.left ) {
				move_vec = Vector3.right;
			} else {
				move_vec = Vector3.left;
			}
		}
	}

	void AnimatorUpdate( ) {
		anim.SetBool( "isDead", _isdead );
	}
}
