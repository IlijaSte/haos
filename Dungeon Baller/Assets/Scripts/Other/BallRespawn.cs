using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BallRespawn : MonoBehaviour {

	public Transform t;
	public Rigidbody rb;
	public float respawnX;
	public float respawnY;
	public float respawnZ;
	public string nextLevel;
	static public string staticNext;
	public PlaySimulation ps;
	public CanvasGroup panel;
	private float lastPos;
	// Use this for initialization
	void Start () {
		t = GetComponent<Transform>();
		rb = GetComponent<Rigidbody> ();
		staticNext = nextLevel;
		lastPos = respawnY;
		SaveManager.loadGame ();
	}

	static public void respawnBall(){
		GameObject ball = GameObject.Find ("Ball");
		BallRespawn br = ball.GetComponent<BallRespawn> ();
		ball.GetComponent<Transform>().position = new Vector3 (br.respawnX, br.respawnY, br.respawnZ);
		ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
		ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

		if (CollectDetector.collected.Count == 0)
			return;

		foreach (GameObject collectible in CollectDetector.collected) {

			collectible.GetComponent<BoxCollider> ().enabled = true;
			collectible.transform.gameObject.GetComponent<MeshRenderer> ().enabled = true;

		}
		CollectDetector.collected.Clear ();
		CollectDetector.numCollected = 0;
		//GetComponent<BoxCollider> ().enabled = false;
		//transform.gameObject.GetComponent<MeshRenderer> ().enabled = false;

	}



	// Update is called once per frame
	void Update () {

		if (t.position.y < respawnY - 5 && lastPos >= respawnY - 5) {
			lastPos = respawnY;
			//PlaySimulation.isSimActive = false;
			GameObject stars = GameObject.Find("Stars");
			//Image[] starImages = stars.GetComponentsInChildren<Image>();
			int count = stars.transform.childCount;
			int i = 0;
			foreach (Transform star in stars.transform) {
				if (CollectDetector.numCollected > i) {
					star.gameObject.GetComponent<Image> ().sprite = star.parent.gameObject.GetComponent<StarScript>().gotImage;
					i++;
				} else{
					star.gameObject.GetComponent<Image> ().sprite = star.parent.gameObject.GetComponent<StarScript>().notGotImage;
					i++;
				}
			}


			panel.alpha = 1;
			panel.blocksRaycasts = true;
			CollectDetector.collected.Clear ();
			SaveManager.saveGame ();
			//SceneManager.LoadScene(nextLevel);
			return;
		}
			
	}
}
