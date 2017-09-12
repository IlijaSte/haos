using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveCenter : MonoBehaviour {

	private Transform t;
	private BoxCollider bc;
	public GameObject ball;
	private Transform bt;
	private Rigidbody brb;

	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball");
		t = GetComponent<Transform> ();
		bt = ball.GetComponent<Transform> ();
		bc = GetComponent<BoxCollider> ();
		brb = ball.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider col){

		if (col.gameObject.name == "Ball") {
			print (brb.angularVelocity);

			float addX = 0;
			float addZ = 0;
			if (t.right.x != 0)
				addX = 0.1f;
			if (t.right.z != 0)
				addZ = 0.1f;

			//brb.velocity = Vector3.zero;
			//brb.angularVelocity = Vector3.zero;

			brb.angularVelocity = Vector3.Scale (brb.angularVelocity, new Vector3 (Mathf.Abs (t.forward.x), 0, Mathf.Abs (t.forward.z)));

			brb.velocity = Vector3.Scale (brb.velocity, new Vector3 (Mathf.Abs (t.right.x), 0, Mathf.Abs (t.right.z)));
			bt.forward = new Vector3 (t.right.x, 0, t.right.z); 

			print (brb.angularVelocity);

			if (Mathf.Abs (brb.velocity.x) > Mathf.Abs (brb.velocity.z)) {
				//brb.velocity.z = Mathf.Round(
				bt.position = new Vector3 (bt.position.x, bt.position.y, Mathf.Round (bt.position.z));
			} else {
				bt.position = new Vector3 (Mathf.Round(bt.position.x), bt.position.y, bt.position.z);
			}

		}

	}
		
}
