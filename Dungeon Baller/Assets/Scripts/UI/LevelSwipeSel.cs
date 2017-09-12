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
	private int dir = 0;
	private float rotateAngle = 0f;
	public bool movedByOffset;
	private GameObject oldPos;
	private GameObject newPos;
	// Use this for initialization
	void Start () {

		camtr = camera.GetComponent<Transform> ();
		rotSpeed = 100f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!moving) {
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {

					initTouch = touch;

				} else if (touch.phase == TouchPhase.Moved) {

					float deltaX = initTouch.position.x - touch.position.x;

					if (deltaX > 0) {

						if (dir == 1) {
							i = 0f;
							dir = -1;
						}
						else
							i += rotSpeed * Time.deltaTime;
						if (i > 0.25f) {

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
							rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

							initTouch = new Touch ();
						}

					} else if (deltaX < 0) {
						if (dir == -1) {
							i = 0;
							dir = 1;
						}
						else
							i -= rotSpeed * Time.deltaTime;
						if (i < -0.25f) {

							moving = true;
							i = 0f;

							dir = 1;

							nextPos = curPos + 1;
							if (nextPos == camPositions.transform.childCount)
								nextPos = 0;

							oldPos = camPositions.transform.GetChild (curPos).gameObject;
							newPos = camPositions.transform.GetChild (nextPos).gameObject;
							selectButton.enabled = false;
							rotateAngle = newPos.transform.rotation.y - oldPos.transform.rotation.y;

							initTouch = new Touch ();
						}


					}
					initTouch = touch;

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

			float d = Mathf.Abs(a - b) % 360; 
			float r = d > 180 ? 360 - d : d;

			//calculate sign 
			int sign = (a - b >= 0 && a - b <= 180) || (a - b <=-180 && a- b>= -360) ? 1 : -1; 
			r *= sign;

			float offset = movedByOffset ? -0.5f : 0;

			if (Mathf.Abs(r) > 0.25f) {
			//if(i < 1f){
				camtr.RotateAround (new Vector3 (offset, camtr.position.y, offset), Vector3.up, dir * tmp);
				//camtr.rotation = Quaternion.Euler (Vector3.Lerp (camtr.rotation.eulerAngles, newPos.transform.rotation.eulerAngles, dir * tmp));

			} else {
				moving = false;
				selectButton.enabled = true;
				curPos = nextPos;
			}

		}
	}
}
