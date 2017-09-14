using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour {

	public static int totalNumCollected = 0;
	public int numCollected = 0;

	public List<int> tempCollected = new List<int>();
	public List<int> collected = new List<int>();
	public static List<int>[] allCollected = new List<int>[256];

	// Use this for initialization
	void Start () {

		SaveManager.loadGame ();
		int i = 0;
		LevelNameHolder nameHolder = GameObject.Find ("GameController").GetComponent<LevelNameHolder> ();
		GameObject collectibles = GameObject.Find ("Collectibles");
		if (allCollected [int.Parse (nameHolder.levelName)] != null) {
			foreach (int index in allCollected[int.Parse(nameHolder.levelName)]) {

				collectibles.transform.GetChild (index).GetComponent<MeshRenderer> ().enabled = false;
				collectibles.transform.GetChild (index).GetComponent<BoxCollider> ().enabled = false;
				i++;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
