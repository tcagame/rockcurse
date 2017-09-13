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

	string CurrentSceneIndex;
	string _current_scene;
	string _next_scene;

	private void Awake( ) {
		new GameObject( ).AddComponent< SceneNavigator >( );
		state = GAME_STATE.PLAY;

		checkSceneIndex( );
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
			_next_scene = "Title";
			break;
		}
		return _next_scene;
	}

	private void deadUpdate( ) {
		if ( state == GAME_STATE.DEAD ) {
			//ゲームオーバーへ遷移
			SceneNavigator.Instance.Change( "GameOver", 1.0f );
		} else {
			state = GAME_STATE.PLAY;
		}
	}

	private void goalUpdate( ) {
		if ( state == GAME_STATE.GOAL ) {
			//次のマップへ遷移
			SceneNavigator.Instance.Change( _next_scene, 2.0f );
			Resources.UnloadUnusedAssets( );
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
}