using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;

	const float SPEED = 5.0f;
	const float DEATH_FALLSPD = -10.0f;
	const float MAX_VEL_X = 3.0f;
	const int GENROCK_COUNT = 3;
	const float FLAP = 250.0f;

	bool jump;
	bool attach;
	bool fall_death;
	bool operate_range;
	bool generate;
	float axis;
	float axis_x;

	Vector3 PUSH_POS;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		anim = GetComponent< Animator >( );
	}

	void Start ( ) {
		jump = true;
		operate_range = false;
		generate = false;
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
		if ( Input.GetButtonDown("A") && !jump ) {
			rb2d.AddForce( Vector3.up * FLAP );
			jump = true;
		}

		generateRock( );
		movingRock( );
	}

	// 岩押し引き
	void movingRock( ) {
		if ( Input.GetButtonDown("B") && operate_range ) {
			
		}
	}

	// 岩生成
	void generateRock( ) {
		GameObject[ ] rock_num = GameObject.FindGameObjectsWithTag ("Rock");

		if ( Input.GetButtonDown("X") ) {
			if ( rock_num.Length < GENROCK_COUNT ) {
				generate = true;
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
			fall_death = true;
		}
	}

	void dead( ) {
		Destroy( gameObject );

		FadeManager.Instance.LoadScene( "GameOver", 1.0f );
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Floor" || other.gameObject.name == "Upside") {
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

	void AnimatorUpdate( ) {
		float walk_speed = rb2d.velocity.x;
		if ( walk_speed < 0 ) {
			walk_speed *= -1;
		}
		anim.SetFloat( "WalkSpeed", walk_speed );

		if ( generate ) {
			anim.SetBool( "Generate", generate );
		}
	}
}