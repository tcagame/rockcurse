using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;
	AnimatorStateInfo animstate;
	SpriteRenderer sr;
	Color color;
    GameObject Audio;

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
    float sound_span;
    //float WaitTime = 0.5f;

    void Awake( ) {
        Audio = GameObject.Find("Audio");
        rb2d = GetComponent< Rigidbody2D >( );
		anim = GetComponent< Animator >( );
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
	
	void Update ( ) {
		if( Time.timeScale > 0 ) {
			animstate = anim.GetCurrentAnimatorStateInfo(0);
			duration = animstate.length;
			anim_nomalized_time = animstate.normalizedTime;
	
			setLookVec( );
			ActionUpdate( );
			AnimatorUpdate( );
		}
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
		rb2d.AddForce( new Vector3( 1000.0f, 1500.0f, 0 ) );
	}

	private void dead( ) {
		_isdead = true;
        sound_span -= Time.deltaTime; //タイマーのカウントダウン
        if (sound_span <= 0) {
            AudioControl se = Audio.GetComponent<AudioControl>();
            se.Playse("敵にあてた");
            sound_span = 3.0f;
         }

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
		     other.gameObject.tag == "Rock" && frontctrl._jump ) {
			jump ();
			//Invoke("jump", WaitTime);
			//動きとめないと吹っ飛ぶ,,,
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
