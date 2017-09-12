﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopView : MonoBehaviour {

	private Vector3 oldCamPos;
	private Quaternion oldCamRot;
	public bool isTop = false;
	private GameObject mainCam;
	// Use this for initialization
	public Material transpMat;
	public Material normMat;
	public GameObject levelObj;
	public GameObject transpObj;
	public Transform transpWall;
	public float speed = 0.1f;
	public Transform newTransform;
	private float i = 0f;
	private bool movingOut = false;
	private bool movingIn = false;
	private Vector3 newPos;
	private Vector3 newRot;
	private Rigidbody rb;
	public float topRotY = 90f;
	void Start () {

		mainCam = GameObject.Find ("Main Camera");
		//levelObj = GameObject.Find ("KULA");
		transpWall = transpObj.GetComponent<Transform>();
		oldCamPos = mainCam.transform.position;
		oldCamRot = mainCam.transform.rotation;
		newPos = new Vector3 (newTransform.position.x, newTransform.position.y + 7, newTransform.position.z);
		newRot = new Vector3 (90f, topRotY, 0);
		rb = mainCam.GetComponent<Rigidbody> ();
	}
		
	public void changeToTop(GameObject go){

		if (!isTop) {
			
			if (!movingOut && !movingIn) {
				i = 0f;
				isTop = true;
				movingOut = true;
				go.GetComponent<MeshRenderer> ().enabled = false;
				
			}
		} else {
			if (!movingIn && !movingOut) {
				i = 0f;
				movingIn = true;
				isTop = false;
				go.GetComponent<MeshRenderer> ().enabled = true;
				transpWall.GetComponent<MeshRenderer> ().material = transpMat;
			}
		}

	}

	void FixedUpdate(){

		if (movingOut) {
			if (i < 1.0f) {
				i += Time.deltaTime * speed;
				rb.MovePosition (Vector3.Lerp (oldCamPos, newPos, i));
				rb.MoveRotation (Quaternion.Euler (Vector3.Lerp (oldCamRot.eulerAngles, newRot, i)));
			} else {
				movingOut = false;
				transpWall.GetComponent<MeshRenderer> ().material = normMat;
				i = 0f;
			}
		} else if (movingIn) {
			if (i < 1.0f) {
				i += Time.deltaTime * speed;

				rb.MovePosition (Vector3.Lerp (newPos, oldCamPos, i));
				rb.MoveRotation (Quaternion.Euler (Vector3.Lerp (newRot, oldCamRot.eulerAngles, i)));

			} else {
				movingIn = false;
				i = 0f;
				//moving = false;
			}
		}

	}
}
