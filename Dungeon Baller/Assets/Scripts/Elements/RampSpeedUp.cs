using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSpeedUp : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Ball") {

			Rigidbody brb = col.gameObject.GetComponent<Rigidbody> ();

			if (Mathf.Abs (brb.velocity.x) > Mathf.Abs (brb.velocity.z)) {

				brb.velocity += new Vector3 (Mathf.Sign(brb.velocity.x) * 2, 0, 0);
			} else {

				brb.velocity += new Vector3 (0, 0, Mathf.Sign(brb.velocity.z) * 2);
			}

		}
	}
}
