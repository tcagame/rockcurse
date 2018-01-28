using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;
	GameManager gm;
	AnimatorStateInfo animstate;
    GameObject Audio;

    const float SPEED = 6.0f;
	const float DEATH_FALLSPD = -10.0f;
	const float MAX_VEL_X = 3.8f;
	const int GENROCK_COUNT = 10;
	const float FLAP = 250.0f;

	bool jump;
	bool attach;
	bool fall_death;
	bool operate_range;
	bool generate;
	bool _rock;
	bool isdead;
	bool inputcut;
	float axis;
	float axis_x;
	float duration;
	float anim_nomalized_time;
    float walk_speed;
    float sound_span;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		anim = GetComponent< Animator >( );
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameManager >( );
	}

	void Start ( ) {
		jump = true;
		operate_range = false;
		generate = false;
		_rock = true;
		isdead = false;
		axis = 0;
		axis_x = 0;
        Audio = GameObject.Find("Audio");
    }

	void Update ( ) {
		if( Time.timeScale > 0 ) {
			axis_x = Input.GetAxis("Horizontal");
			animstate = anim.GetCurrentAnimatorStateInfo(0);
			duration = animstate.length;
			anim_nomalized_time = animstate.normalizedTime;

			if ( transform.position.y < -20.0f ) {
				gm.playerDead( );
			}

			ActionUpdate( );
			getFallSpeed( );
			AnimatorUpdate( );
			waitDeadAnimation( );
			//Confirmation ();
		}
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
        // 移動
        if ( axis_x > 0 && !inputcut ) {
			axis = SPEED * axis_x;
			rb2d.AddForce ( Vector3.right * axis );
			transform.LookAt( transform.position + Vector3.back );
			WalkAudio( );
        }

		if ( axis_x < 0 && !inputcut ) {
			rb2d.AddForce ( Vector3.left * SPEED );
			transform.LookAt( transform.position + Vector3.forward );
			WalkAudio( );
        }

		// デバッグ用キーボード移動対応
		if ( Input.GetKey( KeyCode.LeftArrow ) && !inputcut ) {
			rb2d.AddForce ( Vector3.left * SPEED );
			transform.LookAt( transform.position + Vector3.forward );
			WalkAudio( );
        }
		if ( Input.GetKey( KeyCode.RightArrow ) && !inputcut ) {
			rb2d.AddForce ( Vector3.right * SPEED );
			transform.LookAt( transform.position + Vector3.back );
			WalkAudio( );
        }

		// ジャンプ
		if ( Input.GetButtonDown("A") && !jump && !inputcut ) {
			rb2d.AddForce( Vector3.up * FLAP );
			jump = true;
		}

		if ( Input.GetButtonDown("LB") ) {
			gm.playerDead( );
		}

		SelectRock( );
		generateRock( );
	}

    //岩の選択
    public void SelectRock() {
        if (Input.GetButtonDown("LB") && !_rock) {
            _rock = true;
        }
        if (Input.GetButtonDown("RB") && _rock) {
            _rock = false;
        }
    } 

	// 岩生成
	void generateRock( ) {
		GameObject[ ] rock_num = GameObject.FindGameObjectsWithTag ("Rock");
		AudioControl se1 = Audio.GetComponent< AudioControl >( );

		if ( Input.GetButtonDown ("X") && !inputcut && !jump && _rock ) {
			 if ( rock_num.Length < GENROCK_COUNT ) {
				generate = true;
				inputcut = true;
			}
		}

		// 生成アニメーション待ちして生成
		if (generate && duration >= 1.0f && anim_nomalized_time >= 0.5f) {
			se1.Playse( "Generate" );
			GameObject rock = (GameObject)Resources.Load ("Prefab/Rock");
			Instantiate (rock, transform.position + transform.right * -2.0f, Quaternion.identity);

			generate = false;
			inputcut = false;
		}
	}

	void getFallSpeed( ) {
		float fall_vel = rb2d.velocity.y;

		if ( fall_vel < DEATH_FALLSPD ) {
			fall_death = true;
		}
	}

	void dead( ) {
		jump = false;
		isdead = true;
		inputcut = true;
	}

	void waitDeadAnimation( ) {
		if ( isdead && duration >= 3.0f && anim_nomalized_time >= 0.9f ) {
			gm.playerDead( );
			inputcut = false;
		}
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Floor" || 
			other.gameObject.name == "Upside" ||
			other.gameObject.tag == "Switch" ||
			other.gameObject.tag == "Block" ) {

			if ( fall_death ) {
				dead( );
			}

			jump = false;
		}

		if ( other.gameObject.tag == "Enemy" ) {
			dead( );
		}
	}

	// 生成岩当たり判定
	public void RelayOnTrigger( Collider2D other, int pos ) {
		if ( pos == 1 ) { // 上
			jump = false;
		}
		if ( pos == 2 ) { // 左
			operate_range = true;
		}
		if ( pos == 3 ) { // 右
			operate_range = true;
		}
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.tag == "Goal" ) {
			gm.playerGoal( );
		}
	}

	void AnimatorUpdate( ) {
		walk_speed = rb2d.velocity.x;
		if ( walk_speed < 0 ) {
			walk_speed *= -1;
		}
		anim.SetFloat( "WalkSpeed", walk_speed );

		anim.SetBool( "isJump", jump );
		anim.SetBool( "isGenerate", generate );
		anim.SetBool( "isDead", isdead );
    }

	public void WalkAudio( ) {
		if ( walk_speed > 0.5 && !jump ) {
			sound_span -= Time.deltaTime; //タイマーのカウントダウン
			if ( sound_span <= 0 ) {
				AudioControl se = Audio.GetComponent< AudioControl >( );
				se.Playse( "足音" );
				sound_span = 0.5f;
			}
		}
	}

}