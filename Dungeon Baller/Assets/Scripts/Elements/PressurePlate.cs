using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

	public GameObject linkedTo;
	private bool movingIn = false;
	private bool movingOut = false;
	private float i = 0f;
	private float j = 0f;
	private bool linkedMovingIn = false;
	private bool linkedMovingOut = false;
	private Rigidbody linkedrb;

	private Vector3 initPos;
	private Vector3 endPos;
	private Vector3 linkedInitPos;
	private Vector3 linkedEndPos;
	public float speed = 2f;
	private Rigidbody rb;
	public float moveBy;

	public bool isGoingUp;

	void OnTriggerEnter(Collider col){

		if (col.gameObject.name == "Ball") {
			if (!movingIn) {
				movingIn = true;
				linkedMovingIn = true;
			}

		}

	}

	// Use this for initialization
	void Start () {

		initPos = transform.position;
		endPos = transform.position - (new Vector3 (0, 0.055f, 0));
		linkedInitPos = linkedTo.transform.position;

		if(isGoingUp)
			linkedEndPos = linkedTo.transform.position +(new Vector3 (0, moveBy, 0));
		else 
			linkedEndPos = linkedTo.transform.position -(new Vector3 (0, moveBy, 0));

		rb = GetComponent<Rigidbody> ();
		linkedrb = linkedTo.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!PlaySimulation.isSimActive) {

			linkedTo.transform.position = linkedInitPos;

		}

		if (movingIn) {

			if (i < 1.0f) {

				i += Time.deltaTime * speed;
				rb.MovePosition (Vector3.Lerp (initPos, endPos, i));

				linkedrb.MovePosition (Vector3.Lerp (linkedInitPos, linkedEndPos, i));

			} else {
				movingIn = false;
				movingOut = true;
				linkedMovingIn = false;
				i = 0f;
			}

		} else if (movingOut) {

			if (i < 1.0f) {

				i += Time.deltaTime * speed;
				rb.MovePosition (Vector3.Lerp (endPos, initPos, i));

			} else {
				
				movingOut = false;
				i = 0f;
			}

		}

	}
}
