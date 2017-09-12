using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvOpen : MonoBehaviour {

	public bool open = false;
	public GameObject panel;
	public GameObject canvas;
	private RectTransform rt;

	void Start(){
		

	}
	public void MoveInvPanel(){
		rt = canvas.GetComponent<RectTransform> ();
		if (!open) {
			
			open = true;

			panel.transform.Translate (-panel.transform.right * (135 * canvas.GetComponent<Canvas> ().scaleFactor));
		} else {

			open = false;
			panel.transform.Translate (panel.transform.right * (135 * canvas.GetComponent<Canvas> ().scaleFactor));

		}

	}
		
}
