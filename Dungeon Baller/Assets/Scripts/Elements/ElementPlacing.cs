﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ElementPlacing : MonoBehaviour {

	static public string currHold;
	static public bool holding;
	public GameObject setDir;
	public GameObject block;
	public GameObject ramp;
	public GameObject curve;
	public GameObject pistonBlock;
	public int setdirNum, blockNum, rampNum, curveNum, pistonBlockNum;
	public GameObject ground = null;
	public GameObject rampGround = null;
	private GameObject invPanel;
	private GameObject spawnedObjects;
	static public GameObject LeftRotButton;
	static public GameObject RightRotButton;
	static public GameObject CheckButton;
	static public GameObject RemoveButton;
	static public Image leftImage;
	static public Image rightImage;
	static public Image checkImage;
	public GameObject tower;
	public GameObject transpWall;
	public bool removeToggle;
	public GameObject panel;
	public GameObject canvas;

	//private ArrayList placedObjects;

	// Use this for initialization
	void Awake () {

		//placedObjects = new ArrayList ();
		LeftRotButton = GameObject.Find ("RotLeftButton");
		RightRotButton = GameObject.Find ("RotRightButton");
		CheckButton = GameObject.Find ("CheckButton");
		RemoveButton = GameObject.Find ("RemoveButton");
		leftImage = LeftRotButton.GetComponent<Image> ();
		rightImage = RightRotButton.GetComponent<Image> ();
		checkImage = CheckButton.GetComponent<Image> ();
		if(ground == null)
			ground = GameObject.Find ("PlacingPlanes");
		if (rampGround == null)
			rampGround = GameObject.Find ("RampPlacingPlanes");
		holding = false;
		setdirNum = blockNum = rampNum = curveNum = pistonBlockNum = 0;
		invPanel = GameObject.Find ("InvPanel");
		spawnedObjects = GameObject.Find ("SpawnedObjects");
		removeToggle = false;
	}

	public void TakeElement(string elem){
		if (elem == "ramp") {
			BlockHover.showRampGrid ();
			BlockHover.hideGrid ();
		} else {
			BlockHover.showGrid ();
			BlockHover.hideRampGrid ();
		}
		holding = true;
		currHold = elem;
		if (elem == "remove") {

			removeToggle = !removeToggle;
			if (!removeToggle) {
				holding = false;
				//currHold = "";
			}

		}

		//print (currHold);

		var manager = GameObject.Find ("UIManager");
		if (manager.GetComponent<InvOpen> ().open) {
			manager.GetComponent<InvOpen> ().open = false;
			panel.transform.Translate (panel.transform.right * 135 * canvas.GetComponent<Canvas>().scaleFactor);
			panel.transform.Find ("InventoryArrow").Rotate (0, 0, 180);
		}

	}

	public int getObjectNum(string name){

		/*int num = 0;

		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Spawned Objects")) {

			if (g.name.Contains (name))
				num++;

		}

		return num;*/

		switch (name) {

		case "block":
			return blockNum;
		case "setdir":
			return setdirNum;
		case "ramp":
			return rampNum;
		case "curve":
			return curveNum;
		case "pistonblock":
			return pistonBlockNum;

		}

		return 0;

	}

	public static string truncateNumbers(string name){
		string nameRoot = name;

		while((nameRoot[nameRoot.Length - 1] >= '0') && (nameRoot[nameRoot.Length - 1] <= '9'))
			nameRoot = nameRoot.Remove(nameRoot.Length - 1);

		return nameRoot;
	}

	public void decNum(string name){
		string nameRoot = truncateNumbers(name);
		//while((nameRoot[nameRoot.Length - 1] >= '0') && (nameRoot[nameRoot.Length - 1] <= '9'))
		//	nameRoot = nameRoot.Remove(nameRoot.Length - 1);
		//print (nameRoot);
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Spawned Objects")) {

			if (g.name.Contains (nameRoot)) {

				switch (nameRoot) {
				case "block":
					blockNum--;
					break;
				case "setdir":
					setdirNum--;
					break;
				case "ramp":
					rampNum--;
					break;
				case "curve":
					curveNum--;
					break;
				case "pistonblock":
					pistonBlockNum--;
					break;
				}
				return;

			}

		}

	}

	void activateButtons(){

		Positioning.showButtons ();


	}

	public bool overlaping(Vector3 point){

		point = new Vector3 (Mathf.Round (point.x), point.y, Mathf.Round (point.z));

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spawned Objects")) {
			
			if ((obj.transform.position.x == point.x) && (obj.transform.position.z == point.z)) {

				return true;

			}
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Pre-placed Objects")) {

			if ((obj.transform.position.x == point.x) && (obj.transform.position.z == point.z)) {

				return true;

			}

		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Collectibles")) {

			if ((obj.transform.position.x == point.x) && (obj.transform.position.z == point.z)) {

				return true;

			}

		}

		return false;

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			if (!PlaySimulation.isSimActive && (!EventSystem.current.IsPointerOverGameObject())) {

				Vector2 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				Ray ray;
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				foreach (Transform child in ground.transform) {
					child.GetComponent<MeshCollider> ().enabled = true;
				}
				if (rampGround != null) {
					foreach (Transform child in rampGround.transform) {
						child.GetComponent<MeshCollider> ().enabled = true;
					}
				}
				//ground.GetComponent<MeshCollider> ().enabled = true;
				//rampGround.GetComponent<MeshCollider> ().enabled = true;
			
				BlockHover.hideGrid ();
				BlockHover.hideRampGrid ();
				if (Physics.Raycast (ray, out hit, 100)) {
					bool placed = false;
					if (holding) {
						//print (hit.collider.gameObject.transform.parent.name);
						if (hit.collider.gameObject != null && (hit.collider.gameObject.tag == "Spawned Objects")) {

							Positioning.showButtons ();
							//if (hit.collider.gameObject.name.Contains ("halfcurve")) {
							//	Positioning.placedElem = hit.collider.gameObject.transform.parent.gameObject;
							//}else
							Positioning.placedElem = hit.collider.gameObject;

						} else {
							bool hitGround = false;
							bool hitRampGround = false;
							foreach (Transform child in ground.transform) {
								Bounds bounds = child.GetComponent<MeshCollider> ().bounds;
								if (bounds.Contains (hit.point)) {
									hitGround = true;
									break;
								}
							}

							if (!hitGround && rampGround != null) {

								foreach (Transform child in rampGround.transform) {
									Bounds bounds = child.GetComponent<MeshCollider> ().bounds;
									if (bounds.Contains (hit.point)) {
										hitRampGround = true;
										break;
									}
								}

							}
								
							//if ((bounds.Contains (hit.point) || ((rampGround != null) && rbounds.Contains(hit.point))) && !overlaping(hit.point)) {
							if((hitGround || hitRampGround) && !overlaping(hit.point)){	
								var script = invPanel.GetComponent<AvailElemManager> ();


								GameObject newObj = null;

								if (currHold == "setdir" && hitGround) {
								
									newObj = Instantiate (setDir);
									setdirNum++;
									newObj.name = "setdir" + setdirNum;
									newObj.GetComponent<MonoBehaviour> ().enabled = false;
									newObj.transform.position = new Vector3 (Mathf.Round (hit.point.x), hit.point.y - 0.49f, Mathf.Round (hit.point.z));
									placed = true;
								}
								else if ((currHold == "ramp") && hitRampGround) {

									BlockHover.showRampGrid ();
									BlockHover.hideGrid ();
									foreach (Transform child in GameObject.Find("RampGridBlocks").transform) {
										if (child.gameObject.GetComponent<BlockHover> ().checkMouseOver()) {
											newObj = Instantiate (ramp);
											rampNum++;
											newObj.name = "ramp" + rampNum;
											newObj.transform.position = new Vector3 (Mathf.Round (hit.point.x), hit.point.y + 0.18f, Mathf.Round (hit.point.z));

											newObj.transform.rotation = child.rotation;
											newObj.transform.position = child.position;
											placed = true;
											break;
										}
									}



								} else if (currHold == "curve" && hitGround) {

									newObj = Instantiate (curve);
									curveNum++;
									newObj.name = "curve" + curveNum;
									newObj.transform.position = new Vector3 (Mathf.Round (hit.point.x), hit.point.y + 0.375f - 0.12f, Mathf.Round (hit.point.z));
									placed = true;

								
								} else if (currHold == "pistonblock" && hitGround) {

									newObj = Instantiate (pistonBlock);
									pistonBlockNum++;
									newObj.name = "pistonblock" + pistonBlockNum;
									newObj.transform.position = new Vector3 (Mathf.Round (hit.point.x), hit.point.y + 0.45f, Mathf.Round (hit.point.z));
									placed = true;

								}
								if (placed && newObj) {

									newObj.transform.parent = spawnedObjects.transform;
									activateButtons ();
									//placedObjects.Add (newObj.transform.position);

									Positioning.placedElem = newObj;
										
								}
							}
						}
						
					} //else {
						
					if (!placed && hit.collider.gameObject && (hit.collider.gameObject.tag == "Spawned Objects")) {

						activateButtons ();
						if (hit.collider.gameObject.name.Contains ("ramp")) {
							BlockHover.showRampGrid ();
							BlockHover.hideGrid ();
						} else {
							BlockHover.showGrid ();
							BlockHover.hideRampGrid ();
						}
						//if (hit.collider.gameObject.name.Contains ("halfcurve")) {
							
						//	Positioning.placedElem = hit.collider.gameObject.transform.parent.gameObject;
							//print (hit.collider.gameObject.transform.gameObject.name);
						//}else
						Positioning.placedElem = hit.collider.gameObject;

					}

					//}

				}
				foreach (Transform child in ground.transform) {
					child.GetComponent<MeshCollider> ().enabled = false;
				}
				if (rampGround != null) {
					foreach (Transform child in rampGround.transform) {
						child.GetComponent<MeshCollider> ().enabled = false;
					}
				}
				if (holding) {
					if (currHold == "ramp") {
						BlockHover.showRampGrid ();
						BlockHover.hideGrid ();
					} else {
						BlockHover.showGrid ();
						BlockHover.hideRampGrid ();
					}
				}
			}

		}
	}

}
