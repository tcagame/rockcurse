using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;

	const float SPEED = 5.0f;
	const float DEATH_FALLSPD = -9.5f;
	const float MAX_VEL_X = 3.0f;
	const int GENROCK_COUNT = 2;
	const float flap = 330.0f;

	bool jump;
	bool attach;
	bool fall_death;
	bool operate_range;
	float axis;
	float axis_x;

	Vector3 push_pos;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >();
		anim = GetComponent< Animator >();
	}

	void Start () {
		jump = true;
		operate_range = false;
		axis = 0;
		axis_x = 0;
	}

	void Update () {
		axis_x = Input.GetAxis("Horizontal");

		ActionUpdate( );
		getFallSpeed( );
		AnimatorUpdate( );
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

		if ( axis_x < 0) {
			rb2d.AddForce ( Vector3.left * SPEED );
			transform.LookAt( transform.position + Vector3.forward );
		}

		// ジャンプ
		if ( Input.GetButtonDown("Fire1") && !jump ) {
			rb2d.AddForce( Vector3.up * flap );
			jump = true;
		}

		generateRock();
		movingRock( );
	}

	// 岩押し引き
	void movingRock( ) {
		if ( Input.GetButtonDown("Fire2") && operate_range ) {
			
		}
	}

	// 岩生成
	void generateRock( ) {
		GameObject[] rock_num = GameObject.FindGameObjectsWithTag ("Rock");

		if ( Input.GetButtonDown("Fire3") ) {
			if ( rock_num.Length < GENROCK_COUNT  ) {
				GameObject rock = (GameObject)Resources.Load ("Prefab/Rock");
				Instantiate (rock, transform.position + transform.right * -4.0f, Quaternion.identity);
			}
		}
	}

	void getFallSpeed( ) {
		float fall_vel = rb2d.velocity.y;

		if ( fall_vel < DEATH_FALLSPD ) {
			fall_death = true;
		}
	}

	void dead( ) {
		Destroy( gameObject );

		SceneManager.LoadScene("GameOver");
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Floor" || other.gameObject.name == "Upside") {
			if ( fall_death ) {
				dead( );
			}

			jump = false;
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
	}
}