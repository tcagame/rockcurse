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
	const float GEN_WAITTIME = 2.0f;

	bool jump;
	bool attach;
	bool fall_death;
	bool generate;
	bool gen_wait;
	bool _rock;
	bool isdead;
	bool inputcut;
	bool under_hitting;
	float axis;
	float axis_x;
	float duration;
	float anim_nomalized_time;
    float walk_speed;
    float sound_span;
	float gen_time;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		anim = GetComponent< Animator >( );
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameManager >( );
		Audio = GameObject.Find("Audio");
	}

	void Start ( ) {
		jump = true;
		generate = false;
		gen_wait = false;
		_rock = true;
		isdead = false;
		axis = 0;
		axis_x = 0;
		gen_time = 0;
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

			if ( gen_wait ) {
				gen_time += Time.deltaTime;
				if ( gen_time > GEN_WAITTIME ) {
					gen_wait = false;
					gen_time = 0;
				}
			}

			ActionUpdate( );
			getFallSpeed( );
			AnimatorUpdate( );
			waitDeadAnimation( );
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
		if ( Input.GetButtonDown("A") && !jump && !inputcut && under_hitting ) {
			rb2d.AddForce( Vector3.up * FLAP );
			jump = true;
		}

		SelectRock( );
		generateRock( );
	}

    //岩の選択
    public void SelectRock( ) {
        if ( Input.GetButtonDown("LB") && !_rock ) {
            _rock = true;
        }
        if ( Input.GetButtonDown("RB") && _rock ) {
            _rock = false;
        }
    } 

	// 岩生成
	void generateRock( ) {
		GameObject[ ] rock_num = GameObject.FindGameObjectsWithTag ("Rock");
		AudioControl se1 = Audio.GetComponent< AudioControl >( );

		if ( Input.GetButtonDown ("X") && !inputcut && !jump && _rock && !gen_wait ) {
			if ( rock_num.Length < GENROCK_COUNT ) {
				generate = true;
				inputcut = true;
				gen_wait = true;
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

	public void relayOnTriggerStay2D( ) {
		under_hitting = true;
	}

	public void relayOnTriggerExit2D( ) {
		under_hitting = false;
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Floor" || 
			other.gameObject.name == "Upside" ||
			other.gameObject.tag == "Switch" ||
			other.gameObject.tag == "Block" && under_hitting ) {

			if ( fall_death ) {
				dead( );
			}

			jump = false;
		}

		if ( other.gameObject.tag == "Enemy" ) {
			dead( );
		}
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.name == "Upside" ) {
			jump = false;
		}

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