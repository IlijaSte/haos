using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

	public Vector3 speed = Vector3.zero;
	void Update () {
		float time = Time.deltaTime;
		transform.Translate (time * speed.x, time * speed.y, time * speed.z);

		if (transform.position.x < -5000 || transform.position.z > 8000) {
			Destroy (gameObject);
		}

	}
}
