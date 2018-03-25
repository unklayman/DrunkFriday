using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class FridayGameObject : NetworkBehaviour {

	public Camera Camera;

	protected void Start(){
		Camera = GetComponentInChildren<Camera> ();
	}
}
