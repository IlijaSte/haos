using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHover : MonoBehaviour {

	private Material mat;

	void Start(){
		mat = GetComponent<MeshRenderer> ().material;
	}

	void OnMouseEnter(){

		mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - 0.25f);

	}

	void OnMouseExit(){

		mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + 0.25f);

	}

	public static void showGrid(){
		GameObject blocks = GameObject.Find ("GridBlocks");
		if (blocks == null)
			return;
		foreach (Transform child in blocks.transform) {

			child.GetComponent<MeshRenderer> ().enabled = true;
			child.GetComponent<MeshCollider> ().enabled = true;

		}
	}

	public static void hideGrid(){
		GameObject gridBlocks = GameObject.Find ("GridBlocks");
		if (gridBlocks == null)
			return;
		foreach (Transform child in gridBlocks.transform) {

			child.GetComponent<MeshRenderer> ().enabled = false;
			child.GetComponent<MeshCollider> ().enabled = false;

		}
	}

}
