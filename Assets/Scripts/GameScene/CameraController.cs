using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Vector3 offset;
	Vector3 shift;
	const float smoothing = 8f;
	const float LENGTH = -10.0f;
	Transform player;

	void Start ( ) {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		offset = transform.position - player.position;
		shift = new Vector3( 0, 1.5f, 0 );
	}

	void Update () {
		if ( player != null ) {
			Vector3 targetCamPos = player.position + offset + shift;
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
	}
}