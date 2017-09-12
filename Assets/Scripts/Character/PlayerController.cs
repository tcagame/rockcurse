using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;
	GameManager gm;

	const float SPEED = 5.0f;
	const float DEATH_FALLSPD = -10.0f;
	const float MAX_VEL_X = 3.0f;
	const int GENROCK_COUNT = 5;
	const float FLAP = 250.0f;

	bool _jump;
	bool _attach;
	bool _fall_death;
	bool _operate_range;
	bool _generate;
	bool _dead;
	float axis;
	float axis_x;

	Vector3 PUSH_POS;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		anim = GetComponent< Animator >( );
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameManager >( );
	}

	void Start ( ) {
		_jump = true;
		_operate_range = false;
		_generate = false;
		axis = 0;
		axis_x = 0;
	}

	void Update ( ) {
		axis_x = Input.GetAxis("Horizontal");

		ActionUpdate( );
		getFallSpeed( );
		//AnimatorUpdate( );
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
		if ( axis_x > 0 ) {
			axis = SPEED * axis_x;
			rb2d.AddForce ( Vector3.right * axis );
			transform.LookAt( transform.position + Vector3.back );
		}

		if ( axis_x < 0 ) {
			rb2d.AddForce ( Vector3.left * SPEED );
			transform.LookAt( transform.position + Vector3.forward );
		}

		// ジャンプ
		if ( Input.GetButtonDown("A") && !_jump ) {
			rb2d.AddForce( Vector3.up * FLAP );
			_jump = true;
		}

		generateRock( );
		movingRock( );
	}

	// 岩押し引き
	void movingRock( ) {
		if ( Input.GetButtonDown("B") && _operate_range ) {
			
		}
	}

	// 岩生成
	void generateRock( ) {
		GameObject[ ] rock_num = GameObject.FindGameObjectsWithTag ("Rock");

		if ( Input.GetButtonDown("X") ) {
			if ( rock_num.Length < GENROCK_COUNT ) {
				_generate = true;
				GameObject rock = (GameObject)Resources.Load ("Prefab/Rock");
				Instantiate (rock, transform.position + transform.right * -2.0f, Quaternion.identity);
			}
		}
	}

	void getFallSpeed( ) {
		float fall_vel = rb2d.velocity.y;

		if ( fall_vel < DEATH_FALLSPD ) {
			if ( transform.position.y < -12.0f ) {
				dead( );
			}
			_fall_death = true;
		}
	}

	void dead( ) {
		gm.playerDead( );
		Destroy( gameObject ); // あとで使う動きと入れ替え
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Floor" || other.gameObject.name == "Upside" || other.gameObject.tag == "Switch" ) {
			if ( _fall_death ) {
				dead( );
			}

			_jump = false;
		}

		if ( other.gameObject.tag == "Enemy" ) {
			dead( );
		}
	}

	// 生成岩当たり判定
	public void RelayOnTrigger( Collider2D other, int pos ) {
		if ( pos == 1 ) { // 上
			_jump = false;
		}
		if ( pos == 2 ) { // 左
			_operate_range = true;
		}
		if ( pos == 3 ) { // 右
			_operate_range = true;
		}
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.gameObject.tag == "Goal" ) {
			gm.playerGoal( );
		}
	}

	void AnimatorUpdate( ) {
		float walk_speed = rb2d.velocity.x;
		if ( walk_speed < 0 ) {
			walk_speed *= -1;
		}
		anim.SetFloat( "WalkSpeed", walk_speed );

		if ( _generate ) {
			anim.SetBool( "Generate", _generate );
		}
	}
}