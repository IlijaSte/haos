using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSwipeSel : MonoBehaviour {

	public int curPos = 0;
	private int nextPos = 1;
	private float i = 0f;
	private float j = 0f;
	private float k = 0f;
	private Touch initTouch = new Touch();
	private float deltaY;
	public GameObject camPositions;
	public float rotSpeed;
	private bool moving = false;
	public Camera camera;
	private Transform camtr;
	public Button selectButton;
	private int dir = 1;
	private float rotateAngle = 0f;
	public bool movedByOffset;
	private GameObject oldPos;
	private GameObject newPos;

	private GameObject dlight;
	private GameObject go = null;

	private float startLightIntensity;
	private float endLightIntensity;

	private bool darken = false;
	private bool brighten = false;
	// Use this for initialization
	void Start () {

		camtr = camera.GetComponent<Transform> ();
		//rotSpeed = 100f;
		i = 0f;
		k = 0f;
		moving = false;
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
		dlight = GameObject.Find ("Directional Light");
		startLightIntensity = dlight.GetComponent<Light> ().intensity;
		endLightIntensity = dlight.GetComponent<Light> ().intensity - 0.6f;

		if (!CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
			dlight.GetComponent<Light> ().intensity = endLightIntensity;
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

	public static int extractNumbers(string s){

		string nums = "";
		foreach (char c in s) {
			if (c >= '0' && c <= '9') {
				nums += c;
			}
		}
		return int.Parse (nums);

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
				//print (deltaX);
				if (deltaX > 0) {

					if (dir == 1) {
						i = 0f;
						dir = -1;
						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}
					else
						//i += rotSpeed * Time.deltaTime;
						i += deltaX;
					initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					if (i > Screen.width / 5) {
						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
						moving = true;
						i = 0f;
						k = 0f;
						dir = -1;

						nextPos = curPos - 1;

						//camtr.rotation = Quaternion.Euler(camtr.rotation.eulerAngles - Vector3.up);

						if (nextPos < 0) {
							nextPos = camPositions.transform.childCount - 1;
						}

						if (!CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
							if(CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ().levelName)])
								darken = true;
							j = 0f;
						} else {
							if(!CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ().levelName)])
								brighten = true;
							j = 0f;
						}

						oldPos = camPositions.transform.GetChild (curPos).gameObject;
						newPos = camPositions.transform.GetChild (nextPos).gameObject;
					

						selectButton.enabled = false;
						selectButton.GetComponent<Image> ().enabled = false;
						selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
						rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

						if (go) {
							camtr.transform.parent = null;
							Destroy (go);
						}
						go = new GameObject ();
						go.transform.position = Vector3.zero;
						go.transform.rotation = Quaternion.Euler(new Vector3(0, camtr.rotation.eulerAngles.y, 0));
						transform.parent = go.transform;

						//initTouch = new Touch ();
					}

				} else if (deltaX < 0) {
					if (dir == -1) {
						i = 0;
						dir = 1;

						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}
					else
						//i -= rotSpeed * Time.deltaTime;
						i += deltaX;
					initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					if (i < -Screen.width / 5) {

						moving = true;
						i = 0f;
						k = 0f;
						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
						dir = 1;

						//camtr.rotation = Quaternion.Euler (camtr.rotation.eulerAngles + Vector3.up);

						nextPos = curPos + 1;

						if (nextPos == camPositions.transform.childCount)
							nextPos = 0;

						if (!CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
							if (CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
								darken = true;
								j = 0f;
							}
							//print (extractNumbers (camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ().levelName));
						} else {
							
							if (!CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (curPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
								brighten = true;
								j = 0f;
							}
						}

						oldPos = camPositions.transform.GetChild (curPos).gameObject;
						newPos = camPositions.transform.GetChild (nextPos).gameObject;
					
						selectButton.enabled = false;
						selectButton.GetComponent<Image> ().enabled = false;
						selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = false;
						rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

						if (go) {
							camtr.transform.parent = null;
							Destroy (go);
						}
						go = new GameObject ();
						go.transform.position = Vector3.zero;
						go.transform.rotation = Quaternion.Euler(new Vector3(0, camtr.rotation.eulerAngles.y, 0));
						transform.parent = go.transform;

						//initTouch = new Touch ();
						//initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					}


				}
				//initTouch = touch;
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				//i = 0;

			}
				
			/*
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
						if (i > Screen.width / 5) {

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
						if (i < -Screen.width / 5) {

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
*/
		}else {

			float tmp = Time.deltaTime;
			k += (tmp);

			float a = camtr.rotation.eulerAngles.y;
			float b = newPos.transform.rotation.eulerAngles.y;



			float r = sub2angles (a, b);

			float p = sub2angles (oldPos.transform.rotation.eulerAngles.y, newPos.transform.rotation.eulerAngles.y);

			float offset = movedByOffset ? -0.5f : 0;



			//if (Mathf.Abs (r) > 2f) {
				//if(i < 1f){
			Vector3 rotA = new Vector3(0, camtr.rotation.eulerAngles.y, 0);
			Vector3 rotB = new Vector3(0, newPos.transform.rotation.eulerAngles.y, 0);

			//if(Vector3.Distance(rotA, rotB) > 1f){
			if(k < 1){
				print (k);
				//camtr.RotateAround (new Vector3 (offset, camtr.position.y, offset), Vector3.up, dir * tmp);
				if (camPositions.transform.childCount == 2) {
					go.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(oldPos.transform.rotation.eulerAngles.y, oldPos.transform.rotation.eulerAngles.y + dir * 180, k), 0);

				}else
					go.transform.rotation = Quaternion.Euler(0, Quaternion.Slerp(oldPos.transform.rotation, newPos.transform.rotation, k).eulerAngles.y, 0);
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
				k = 0f;
				initClickPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				//if(Input.touches.Length > 0)
				//	initTouch = Input.touches[0];
				changedMat = false;
				if (CollectManager.levelsPassed [extractNumbers (camPositions.transform.GetChild (nextPos).gameObject.GetComponent<LevelNameHolder> ().levelName)]) {
					selectButton.enabled = true;
					selectButton.GetComponent<Image> ().enabled = true;
					selectButton.transform.GetChild (0).GetComponent<Text> ().enabled = true;
				}


				curPos = nextPos;
			}

		}

		if (darken) {
			j += Time.deltaTime * 1.25f;
			if (j < 1) {
				dlight.GetComponent<Light> ().intensity = Mathf.Lerp (startLightIntensity, endLightIntensity, j);
			} else {
				darken = false;
				j = 0f;
			}
		} else if (brighten) {
			j += Time.deltaTime * 1.25f;
			if (j < 1) {
				dlight.GetComponent<Light> ().intensity = Mathf.Lerp (endLightIntensity, startLightIntensity, j);
			} else {
				brighten = false;
				j = 0f;
			}
		}
	}
}
