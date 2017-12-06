using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonManager : MonoBehaviour {

	public void startLevel(string name){

		switch (name) {
		case "Floor2Selection":
			if (CollectManager.totalNumCollected >= 1)
				SceneManager.LoadScene (name);
			break;
		case "Floor3Selection":
			if (CollectManager.totalNumCollected >= 4)
				SceneManager.LoadScene (name);
			break;
		default:
			SceneManager.LoadScene (name);
			break;
		}

			

	}
}
