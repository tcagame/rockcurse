using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Animator anim;
	GameManager gm;
	AnimatorStateInfo animstate;

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
	bool rock;
	bool isdead;
	bool inputcut;
	float axis;
	float axis_x;
	float duration;
	float anim_nomalized_time;

	Vector3 PUSH_POS;

	void Awake( ) {
		rb2d = GetComponent< Rigidbody2D >( );
		anim = GetComponent< Animator >( );
		gm = GameObject.FindGameObjectWithTag("GameController").GetComponent< GameManager >( );
	}

	void Start ( ) {
		jump = true;
		operate_range = false;
		generate = false;
		rock = true;
		isdead = false;
		isdead = false;
		axis = 0;
		axis_x = 0;
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
		}

		if ( axis_x < 0 && !inputcut ) {
			rb2d.AddForce ( Vector3.left * SPEED );
			transform.LookAt( transform.position + Vector3.forward );
		}

		// デバッグ用キーボード移動対応
		if ( Input.GetKey( KeyCode.LeftArrow ) && !inputcut ) {
			rb2d.AddForce ( Vector3.left * SPEED );
			transform.LookAt( transform.position + Vector3.forward );
		}
		if ( Input.GetKey( KeyCode.RightArrow ) && !inputcut ) {
			rb2d.AddForce ( Vector3.right * SPEED );
			transform.LookAt( transform.position + Vector3.back );
		}

		// ジャンプ
		if ( Input.GetButtonDown("A") && !jump && !inputcut ) {
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

		if (Input.GetButtonDown ("X") && !inputcut && !jump && rock) {
			if (rock_num.Length < GENROCK_COUNT) {
				generate = true;
				inputcut = true;
			}
		}

		// 生成アニメーション待ちして生成
		if (generate && duration >= 1.0f && anim_nomalized_time >= 0.5f) {
			GameObject rock = (GameObject)Resources.Load ("Prefab/Rock");
			Instantiate (rock, transform.position + transform.right * -2.0f, Quaternion.identity);
			generate = false;
			inputcut = false;
		}
	}

	//仮でやってみたけど色々わからなくて挫折
	void generateStone( ) {	
		GameObject[ ] stone_num = GameObject.FindGameObjectsWithTag("Stone");

		if ( Input.GetButtonDown("X") && !inputcut && !jump && !rock) {
			if ( stone_num.Length < GENROCK_COUNT ) {
				generate = true;
				inputcut = true;
			}
		}

		// 生成アニメーション待ちして生成
		if ( generate && duration >= 1.0f && anim_nomalized_time >= 0.5f ) {
			GameObject stone = (GameObject)Resources.Load ("Prefab/Stone");
			Instantiate( stone, transform.position + transform.right * -2.0f, Quaternion.identity );
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
		float walk_speed = rb2d.velocity.x;
		if ( walk_speed < 0 ) {
			walk_speed *= -1;
		}
		anim.SetFloat( "WalkSpeed", walk_speed );

		anim.SetBool( "isJump", jump );
		anim.SetBool( "isGenerate", generate );
		anim.SetBool( "isDead", isdead );
	}

	//ごめん　わからなかった
	/*void Confirmation( ){
		GameObject ui = GameObject.Find ("UI");
		ItemUI _ui = GetComponent<ItemUI> ();
		if (_ui._itemUI [0]) {
			rock = true;
		} else if (_ui._itemUI [1]) {
			rock = false;
		}
			
	}*/
}