using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBlockActivation : MonoBehaviour {
	
	public WallTrigger piston;

	void OnMouseDown(){

		piston.OnMouseDown ();
	}
}
