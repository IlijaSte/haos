using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

	float currentMusicTime;

	void Start(){
		DontDestroyOnLoad(gameObject);
	}

	void Update(){
		currentMusicTime = GetComponent<AudioSource>().time;
	}

	void OnLevelWasLoaded(int lvl){
		GetComponent<AudioSource>().time = currentMusicTime;
	}
}
