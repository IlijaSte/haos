using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonManager : MonoBehaviour {

	public void startLevel(string name){

		SceneManager.LoadScene (name);

	}
}
