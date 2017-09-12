using UnityEngine;
using System.Collections;

public class Booster : MyGameObject
{
		
	public static int elNum = 0;

	public Booster(GameObject prefab, Vector3 hitpoint) : base(prefab) {
		
		gameObject.name = "booster" + (++elNum);
		gameObject.GetComponent<MonoBehaviour> ().enabled = false;
		gameObject.GetComponent<Transform>().position = new Vector3 (Mathf.Round (hitpoint.x), hitpoint.y - 0.49f, Mathf.Round (hitpoint.z));
	}

}

