using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
	public GameObject target;
	public float rotateSpeed = 5;

	Vector3 offset;

	void Start() {
		//offset = target.transform.position - transform.position;
		offset = new Vector3(0,-5.0f,3);
	}

	void LateUpdate() {
		if (target == null) {
			return;
		}
		//float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
		//target.transform.Rotate (0, horizontal, 0);

		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);

		transform.position = target.transform.position - (rotation * offset);

		transform.LookAt (target.transform);
	}
}
