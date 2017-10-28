using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	enum GAME_STATE {
		PLAY,
		DEAD,
		GOAL,
		MAX_STATE
	};

	enum MAP {
		MAP1,
		MAP2,
		MAP3,
		MAP4,
		MAP5,
		MAP6,
		MAX_MAP
	};

	GAME_STATE state;
	MAP map;

	GameObject gameover;

	string CurrentSceneIndex;
	public string _current_scene;
	string _next_scene;
	bool _gameover;
	public static int countselect = 0;

	private void Awake( ) {
		new GameObject( ).AddComponent< SceneNavigator >( );
		gameover = GameObject.Find("Gameover_BG").gameObject;
		gameover.gameObject.SetActive( _gameover );
		state = GAME_STATE.PLAY;

		checkSceneIndex( );
	}

	private void Start( ) {
		_gameover = false;
	}
	
 	private void Update( ) {
		switch ( state ) {
		case GAME_STATE.PLAY:
			break;
		case GAME_STATE.DEAD:
			deadUpdate( );
			break;
		case GAME_STATE.GOAL:
			goalUpdate( );
			break;
		}
	}

	private void checkSceneIndex( ) {
		CurrentSceneIndex = SceneManager.GetActiveScene( ).name; //現在のシーン名を取得
		_current_scene = CurrentSceneIndex;

		setNextSceneIndex( );
	}

	private string setNextSceneIndex( ) {
		switch( CurrentSceneIndex ) {
		case "map1":
			_next_scene = "map2";
			break;
		case "map2":
			_next_scene = "map3";
			break;
		case "map3":
			_next_scene = "map4";
			break;
		case "map4":
			_next_scene = "map5";
			break;
		case "map5":
			_next_scene = "map6";
			break;
		case "map6":
			_next_scene = "thanks";
			break;
		}
		return _next_scene;
	}

	private void deadUpdate( ) {
		if ( state == GAME_STATE.DEAD ) {
			//ゲームオーバーへ遷移
			_gameover = true;
			GameObject.Find("Canvas").transform.Find("Gameover_BG").gameObject.SetActive( _gameover );
		} else {
			state = GAME_STATE.PLAY;
		}
	}

	private void goalUpdate( ) {
		if ( state == GAME_STATE.GOAL ) {
			//次のマップへ遷移
			if ( _next_scene == "thanks" ) {
				SceneNavigator.Instance.Change( _next_scene, 2.5f );
				Resources.UnloadUnusedAssets( );
			} else {
				SceneNavigator.Instance.Change( _next_scene, 1.5f );
				Resources.UnloadUnusedAssets( );
				countselect++;
			}
		} else {
			state = GAME_STATE.PLAY;
		}
	}

	public void playerDead( ) {
		if ( state != GAME_STATE.DEAD ) {
			state = GAME_STATE.DEAD;
		}
	}

	public void playerGoal( ) {
		if ( state != GAME_STATE.GOAL ) {
			state = GAME_STATE.GOAL;
		}
	}

	public static int CountSelect() {
		return countselect;
	}
}