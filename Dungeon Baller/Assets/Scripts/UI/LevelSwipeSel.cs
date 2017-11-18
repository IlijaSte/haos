using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwipeSel : MonoBehaviour {

	public int curPos = 0;
	private int nextPos = 1;
	private float i = 0f;
	private Touch initTouch = new Touch();
	private float deltaY;
	public GameObject camPositions;
	public float rotSpeed = 8f;
	private bool moving = false;
	public Camera camera;
	private Transform camtr;
	public Button selectButton;
	private int dir = 1;
	private float rotateAngle = 0f;
	public bool movedByOffset;
	private GameObject oldPos;
	private GameObject newPos;
	// Use this for initialization
	void Start () {

		camtr = camera.GetComponent<Transform> ();
		rotSpeed = 100f;

		LevelNameHolder lnm = camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ();
		if (lnm.transpWall) {
			int numOfMats = lnm.transpWall.GetComponent<MeshRenderer> ().materials.Length;
			Material[] mats = new Material[numOfMats];
			for (int j = 0; j < numOfMats; j++) {
				mats [j] = lnm.transpMaterial;
			}
			lnm.transpWall.GetComponent<MeshRenderer> ().materials = mats;
			lnm.transpWall.GetComponent<MeshRenderer> ().material = lnm.transpMaterial;
		}
	}

	private bool isHoldingMouse = false;
	private Vector2 initClickPos;

	float sub2angles(float a, float b){

		float d = Mathf.Abs(a - b) % 360; 
		float r = d > 180 ? 360 - d : d;

		//calculate sign 
		int sign = (a - b >= 0 && a - b <= 180) || (a - b <=-180 && a- b>= -360) ? 1 : -1; 
		r *= sign;
		return r;
	}

	bool changedMat = false;
	// Update is called once per frame
	void FixedUpdate () {


		if (Input.GetMouseButtonDown (0)) {

			isHoldingMouse = true;
			initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			i = 0;
		} else if (Input.GetMouseButtonUp (0)) {

			isHoldingMouse = false;
			i = 0;
		}

		if (!moving) {


			if (Input.GetMouseButton (0)) {

				float deltaX = initClickPos.x - Input.mousePosition.x;
				print (i);
				if (deltaX > 0) {

					if (dir == 1) {
						i = 0f;
						dir = -1;
						initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}
					else
						//i += rotSpeed * Time.deltaTime;
						i += deltaX;
					if (i > 100f) {
						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
						moving = true;
						i = 0f;
						dir = -1;

						nextPos = curPos - 1;
						if (nextPos < 0) {
							nextPos = camPositions.transform.childCount - 1;
						}

						oldPos = camPositions.transform.GetChild (curPos).gameObject;
						newPos = camPositions.transform.GetChild (nextPos).gameObject;
						selectButton.enabled = false;
						selectButton.GetComponent<Image> ().enabled = false;
						selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
						rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

						//initTouch = new Touch ();
					}

				} else if (deltaX < 0) {
					if (dir == -1) {
						i = 0;
						dir = 1;

						initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}
					else
						//i -= rotSpeed * Time.deltaTime;
						i += deltaX;
					if (i < -100f) {

						moving = true;
						i = 0f;
						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
						dir = 1;

						nextPos = curPos + 1;
						if (nextPos == camPositions.transform.childCount)
							nextPos = 0;

						oldPos = camPositions.transform.GetChild (curPos).gameObject;
						newPos = camPositions.transform.GetChild (nextPos).gameObject;
		
						selectButton.enabled = false;
						selectButton.GetComponent<Image> ().enabled = false;
						selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
						rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

						//initTouch = new Touch ();
						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}


				}
				//initTouch = touch;
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				//i = 0;

			}
				

			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {

					initTouch = touch;

				} else if (touch.phase == TouchPhase.Moved) {

					float deltaX = initTouch.position.x - touch.position.x;

					if (deltaX > 0) {

						if (dir == 1) {
							i = 0f;
							dir = -1;
							initTouch = touch;
						}
						else
							i += deltaX;
						if (i > 100f) {

							moving = true;
							i = 0f;
							dir = -1;

							nextPos = curPos - 1;
							if (nextPos < 0) {
								nextPos = camPositions.transform.childCount - 1;
							}

							oldPos = camPositions.transform.GetChild (curPos).gameObject;
							newPos = camPositions.transform.GetChild (nextPos).gameObject;
		
							selectButton.enabled = false;
							selectButton.GetComponent<Image> ().enabled = false;
							selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
							rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

							initTouch = new Touch ();
						}

					} else if (deltaX < 0) {
						if (dir == -1) {
							i = 0;
							dir = 1;
							initTouch = touch;
						}
						else
							i += deltaX;
						if (i < -100f) {

							moving = true;
							i = 0f;

							dir = 1;

							nextPos = curPos + 1;
							if (nextPos == camPositions.transform.childCount)
								nextPos = 0;

							oldPos = camPositions.transform.GetChild (curPos).gameObject;
							newPos = camPositions.transform.GetChild (nextPos).gameObject;

							selectButton.enabled = false;
							selectButton.GetComponent<Image> ().enabled = false;
							selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
							rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

							initTouch = new Touch ();
						}


					}
					//initTouch = touch;

				} else if (touch.phase == TouchPhase.Ended) {

					i = 0f;
					initTouch = new Touch ();

				}
			}
		}else {

			float tmp = rotSpeed * Time.deltaTime;
			i += tmp;

			float a = camtr.rotation.eulerAngles.y;
			float b = newPos.transform.rotation.eulerAngles.y;



			float r = sub2angles (a, b);

			float p = sub2angles (oldPos.transform.rotation.eulerAngles.y, newPos.transform.rotation.eulerAngles.y);

			float offset = movedByOffset ? -0.5f : 0;

			if (Mathf.Abs (r) > 0.25f) {
				//if(i < 1f){
				camtr.RotateAround (new Vector3 (offset, camtr.position.y, offset), Vector3.up, dir * tmp);
				//camtr.rotation = Quaternion.Euler (Vector3.Lerp (camtr.rotation.eulerAngles, newPos.transform.rotation.eulerAngles, dir * tmp));
				LevelNameHolder lnmOld = camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ();
				LevelNameHolder lnmNew = camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ();
				if ((lnmOld.transpWall != null) && (Mathf.Abs (r) < Mathf.Abs (p / 2)) && !changedMat) {
					
					MeshRenderer mr = lnmNew.transpWall.GetComponent<MeshRenderer> ();
					Material[] mats = new Material[mr.materials.Length];
					lnmOld.transpWall.GetComponent<MeshRenderer> ().materials = lnmOld.origMaterials;

					for(int j = 0; j < mr.materials.Length; j++) {

						mats[j] = lnmNew.transpMaterial;
					}
					mr.materials = mats;
					changedMat = true;
				}

			}else{
				moving = false;
				i = 0f;
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				//if(Input.touches.Length > 0)
				//	initTouch = Input.touches[0];
				changedMat = false;
				selectButton.enabled = true;
				selectButton.GetComponent<Image> ().enabled = true;
				selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = true;
				curPos = nextPos;
			}

		}
	}
}
