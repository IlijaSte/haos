using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

	public void StartGame(string sceneName){

		SceneManager.LoadScene (sceneName);

	}

	public void exitGame(){
		Application.Quit ();
	}
}
