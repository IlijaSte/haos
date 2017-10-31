using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class AvailElemManager : MonoBehaviour {

	public GameObject block;
	public GameObject setdir;
	public GameObject ramp;
	public GameObject curve;
	public GameObject pistonBlock;
	private GameObject placing;
	public int blockNum;
	public int curveNum;
	public int setdirNum;
	public int rampNum;
	public int pistonBlockNum;

	public ElementPlacing ep;

	public struct AvailElement{

		public GameObject go;
		public int count;
		public string type;

		public AvailElement(GameObject o, int c, string t){
			go = o;
			count = c;
			type = t;
			go.name = t;
		}
	}

	public List<AvailElement> availElements;

	// Use this for initialization
	void Awake () {



		placing = GameObject.Find ("Placing");
		ep = placing.GetComponent<ElementPlacing> ();
		availElements = new List<AvailElement>();
		//availElements.Add(new AvailElement(block, 2, "block"));
		//availElements.Add(new AvailElement(setdir, 3, "setdir"));
		//availElements.Add(new AvailElement(ramp, 2, "ramp"));
		availElements.Add(new AvailElement(curve, curveNum, "curve"));
		availElements.Add(new AvailElement(setdir, setdirNum, "setdir"));
		availElements.Add(new AvailElement(ramp, rampNum, "ramp"));
		availElements.Add (new AvailElement (pistonBlock, pistonBlockNum, "pistonblock"));
		//availElements.Add (new AvailElement (setdir, 1, "setdir"));

		foreach (AvailElement elem in availElements) {

			GameObject newElem = Instantiate (elem.go) as GameObject;
			newElem.transform.SetParent (GameObject.Find ("Buttons").transform, false);
			newElem.GetComponent<Button> ().onClick.AddListener(delegate {placing.GetComponent<ElementPlacing>().TakeElement(elem.type);});
				

		}


	}

	public int getCount(string name){

		foreach (AvailElement ae in availElements) {

			if (ae.type == name)
				return ae.count;

		}

		return -1;

	}

	// Update is called once per frame
	void Update () {

		//for (int i = 0; i < availElements.Count; i++) {
		foreach(AvailElement ae in availElements){

			GameObject elem = GameObject.Find (ae.type + "(Clone)");

			if (ep.getObjectNum(ae.type) < ae.count) {
				if (elem == null) {
					
					switch (ae.type) {
					case "block":
						elem = block;
						break;
					case "setdir":
						elem = setdir;
						break;
					case "ramp":
						elem = ramp;
						break;
					case "curve":
						elem = curve;
						break;

					case "pistonblock":
						elem = pistonBlock;
						break;
					}
					//ElementPlacing.holding = true;
					//ElementPlacing.currHold = ae.type;
					GameObject newElem = Instantiate (elem) as GameObject;
					newElem.transform.SetParent (GameObject.Find ("Buttons").transform, false);
					newElem.GetComponent<Button> ().onClick.AddListener(delegate{ GameObject.Find("Placing").GetComponent<ElementPlacing>().TakeElement(ae.type); });
					elem = newElem;
				}
					

				elem.transform.GetChild (0).GetComponent<Text> ().text = "x" + (ae.count - ep.getObjectNum (ae.type));

			} else {

				if (elem) {
					//GameObject.Find (ae.go.name + "(Clone)").GetComponent<RectTransform> ().SetAsLastSibling ();
					Destroy (GameObject.Find (ae.go.name + "(Clone)"));
					//

					ElementPlacing.holding = false;
					BlockHover.hideGrid ();
					BlockHover.hideRampGrid ();
				}

			}


		}

	}
}
