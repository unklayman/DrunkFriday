using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController {

	private LookAtCamera camera;

	private MainCameraController(){
		this.camera = Camera.main.GetComponent(typeof(LookAtCamera)) as LookAtCamera;
	}

	private static MainCameraController instance;

	public static MainCameraController GetInstance() {
		if (instance == null) {
			instance = new MainCameraController();
		}
		return instance;
	}

	public void SetTarget(GameObject target){
		if (camera != null && target!=null) {
			camera.SetTarget(target);
		}	
	}

	public Vector3 GetCameraViewVector(){
		return camera.transform.TransformDirection (Vector3.forward);
	}

	public Quaternion GetCameraRotation(){
		return camera.gameObject.transform.rotation;
	}
}
