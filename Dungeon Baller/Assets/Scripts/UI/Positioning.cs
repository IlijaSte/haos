using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Positioning : MonoBehaviour {

	static public bool isPositioning;
	static public GameObject placedElem;
	public ElementPlacing ep;
	private int currX = 0;

	void Awake(){

		isPositioning = false;
		currX = 0;
	}

	public void checkPress(){

		if (isPositioning) {

			hideButtons ();
			BlockHover.hideGrid ();
			BlockHover.hideRampGrid ();
		}

	}

	static public void showButtons(){

		if (!isPositioning) {
			ElementPlacing.CheckButton.GetComponent<Button> ().interactable = true;
			ElementPlacing.LeftRotButton.GetComponent<Button> ().interactable = true;
			ElementPlacing.RightRotButton.GetComponent<Button> ().interactable = true;
			ElementPlacing.CheckButton.GetComponent<RectTransform> ().localPosition += new Vector3 (0, 95, 0);
			ElementPlacing.LeftRotButton.GetComponent<RectTransform> ().localPosition += new Vector3 (0, 95, 0);
			ElementPlacing.RightRotButton.GetComponent<RectTransform> ().localPosition += new Vector3 (0, 95, 0);
			ElementPlacing.RemoveButton.GetComponent<RectTransform> ().localPosition += new Vector3 (0, 95, 0);
			isPositioning = true;
			//currX = 0;
		}
	}

	static public void hideButtons(){

		if (isPositioning) {
			ElementPlacing.CheckButton.GetComponent<Button> ().interactable = false;
			ElementPlacing.LeftRotButton.GetComponent<Button> ().interactable = false;
			ElementPlacing.RightRotButton.GetComponent<Button> ().interactable = false;
			ElementPlacing.CheckButton.GetComponent<RectTransform> ().localPosition -= new Vector3 (0, 95, 0);
			ElementPlacing.LeftRotButton.GetComponent<RectTransform> ().localPosition -= new Vector3 (0, 95, 0);
			ElementPlacing.RightRotButton.GetComponent<RectTransform> ().localPosition -= new Vector3 (0, 95, 0);
			ElementPlacing.RemoveButton.GetComponent<RectTransform> ().localPosition -= new Vector3 (0, 95, 0);

			isPositioning = false;
		}
	}

	public void RotateRight(){
		if (placedElem.GetComponent<ElementProperties> ().rotatable) {

			placedElem.transform.rotation = Quaternion.Euler (new Vector3 (placedElem.transform.rotation.eulerAngles.x, placedElem.transform.rotation.eulerAngles.y + 90, placedElem.transform.rotation.eulerAngles.z));
	
		}
	}

	public void RotateLeft(){
		if (placedElem.GetComponent<ElementProperties> ().rotatable) {
			
			placedElem.transform.rotation = Quaternion.Euler (new Vector3 (placedElem.transform.rotation.eulerAngles.x, placedElem.transform.rotation.eulerAngles.y - 90, placedElem.transform.rotation.eulerAngles.z));

		}
	}

	public void RemoveElem(){
		print (placedElem.name);


		if (placedElem.GetComponent<ElementProperties> ().removable) {
			ElementPlacing.currHold = ep.truncateNumbers (placedElem.name);
			ElementPlacing.holding = true;
			if(placedElem.name.Contains("ramp")){
				BlockHover.showRampGrid();
				BlockHover.hideGrid();
			}else{
				BlockHover.showGrid ();
				BlockHover.hideRampGrid();
			}
			ep.decNum (placedElem.name);
			Destroy (placedElem);

		}
		
		
	}

	void Update(){

		if (isPositioning && placedElem == null) {
			hideButtons ();
		}

	}

}
