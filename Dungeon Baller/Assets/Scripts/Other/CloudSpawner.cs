using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	public GameObject cloudPrefab;
	private float i = 0;

	public float spawnSpeed = 0;

	void Start () {
		if (spawnSpeed == 0) {
			Random.InitState ((int)System.DateTime.Now.Ticks);
			spawnSpeed = Random.Range (2, 4);
		}
		if (spawnSpeed == -1)
			spawnSpeed = 0;

		int j = 1;
		if (Random.Range (0, 2) == 1) {
			j++;
		}
		for (int i = 0; i < j; i++) {
			GameObject newCloud = Instantiate (cloudPrefab);
			newCloud.transform.position = new Vector3 (Random.Range(-500, 3000), Random.Range (2000, 3000), Random.Range(-5000, -2000));
			newCloud.transform.localScale = new Vector3 (Random.Range (5, 8) * 50, 50, Random.Range (3, 5) * 50);
			newCloud.transform.rotation = Quaternion.Euler (0, 45, 0);
			float cloudSpeed = Random.Range (25, 150);
			newCloud.GetComponent<CloudMovement> ().speed = new Vector3(-cloudSpeed, 0, cloudSpeed / 4);
		}

		j = 2;
		if (Random.Range (0, 2) == 1) {
			j++;
		}
		for (int i = 0; i < j; i++) {
			GameObject newCloud = Instantiate (cloudPrefab);
			newCloud.transform.position = new Vector3 (Random.Range(-1000, 8000), Random.Range (2000, 3000), Random.Range(-8000, -4000));
			newCloud.transform.localScale = new Vector3 (Random.Range (5, 8) * 50, 50, Random.Range (3, 5) * 50);
			newCloud.transform.rotation = Quaternion.Euler (0, 45, 0);
			float cloudSpeed = Random.Range (25, 150);
			newCloud.GetComponent<CloudMovement> ().speed = new Vector3(-cloudSpeed, 0, cloudSpeed / 4);
		}

	}
	
	void Update () {

		i += Time.deltaTime * spawnSpeed;

		if (i >= 25) {

			GameObject newCloud = Instantiate (cloudPrefab);
			newCloud.transform.position = new Vector3 (Random.Range(-1000, 8000), Random.Range (2000, 3000), Random.Range(-8000, -6000));
			newCloud.transform.localScale = new Vector3 (Random.Range (5, 8) * 50, 50, Random.Range (3, 5) * 50);
			newCloud.transform.rotation = Quaternion.Euler (0, 45, 0);
			float cloudSpeed = Random.Range (25, 150);
			newCloud.GetComponent<CloudMovement> ().speed = new Vector3(-cloudSpeed, 0, cloudSpeed / 4);

			i = 0;
			spawnSpeed = Random.Range (2, 4);
		}

	}
}
