using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController {

	private BaseCamera[] cameras;
	private BaseCamera activeCamera;

	private MainCameraController() {
		cameras = Camera.main.GetComponents<BaseCamera>();
	}

	private static MainCameraController instance;

	public static MainCameraController GetInstance() {
		if (instance == null) {
			instance = new MainCameraController();
		}
		return instance;
	}

	public void SetTargetFor<T> (GameObject gameObject) where T: BaseCamera{
		DisableAllCameras ();
		this.activeCamera = Camera.main.GetComponent(typeof(T)) as T;
		activeCamera.enabled = true;
		activeCamera.Target = gameObject;
	}

	public Vector3 GetCameraViewVector(){
		return activeCamera.transform.TransformDirection (Vector3.forward);
	}

	public Quaternion GetCameraRotation(){
		return activeCamera.gameObject.transform.rotation;
	}

	private void DisableAllCameras ()
	{
		for (var i = 0; i < cameras.Length; i++) {
			cameras [i].enabled = false;
		}
	}
}
