using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHover : MonoBehaviour {

	private Material mat;
	public bool mouseIn = false;

	void Start(){
		mat = GetComponent<MeshRenderer> ().material;
	}

	void OnMouseEnter(){

		mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - 0.25f);
		mouseIn = true;
	}

	void OnMouseExit(){

		mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + 0.25f);
		mouseIn = false;
	}

	public static void showGrid(){
		GameObject blocks = GameObject.Find ("RegularGridBlocks");
		if (blocks == null)
			return;
		foreach (Transform child in blocks.transform) {

			child.GetComponent<MeshRenderer> ().enabled = true;
			child.GetComponent<MeshCollider> ().enabled = true;

		}
	}

	public static void showRampGrid(){
		GameObject blocks = GameObject.Find ("RampGridBlocks");
		if (blocks == null)
			return;
		foreach (Transform child in blocks.transform) {

			child.GetComponent<MeshRenderer> ().enabled = true;
			child.GetComponent<MeshCollider> ().enabled = true;

		}
	}

	public static void hideGrid(){
		GameObject gridBlocks = GameObject.Find ("RegularGridBlocks");
		if (gridBlocks == null)
			return;
		foreach (Transform child in gridBlocks.transform) {

			child.GetComponent<MeshRenderer> ().enabled = false;
			child.GetComponent<MeshCollider> ().enabled = false;

		}
	}

	public static void hideRampGrid(){
		GameObject blocks = GameObject.Find ("RampGridBlocks");
		if (blocks == null)
			return;
		foreach (Transform child in blocks.transform) {

			child.GetComponent<MeshRenderer> ().enabled = false;
			child.GetComponent<MeshCollider> ().enabled = false;

		}
	}

}
