using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectDetector : MonoBehaviour {

	public static int totalNumCollected = 0;
	public static int numCollected = 0;
	public static ArrayList collected = new ArrayList();
	public GameObject ball;
	private Transform transf;
	private int dir = -1;
	private float yoff = 0;

	void Start(){

		transf = GetComponent<Transform> ();

	}
	void Update(){


		if (GetComponent<BoxCollider> ().bounds.Contains (ball.transform.position)) {

			numCollected++;
			totalNumCollected++;
			collected.Add (transform.gameObject);
			GetComponent<BoxCollider> ().enabled = false;
			transform.gameObject.GetComponent<MeshRenderer> ().enabled = false;
			//Destroy (transform.gameObject);

		} else {

			transf.Rotate (new Vector3 (0, -100 * Time.deltaTime, 0));
			float temp = dir * 0.1f * Time.deltaTime;
			yoff += Mathf.Abs (temp);
			transf.Translate (new Vector3 (0, temp, 0));
			if (yoff > 0.15f) {
				dir = -dir;
				yoff = 0;
			}
		}
	}
}
