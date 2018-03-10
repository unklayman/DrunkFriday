using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
	private GameObject target;

	Vector3 offset;

	void Start() {
		offset = new Vector3(0,-2.0f,3);
	}

	void LateUpdate() {
		if (target == null) {
			return;
		}
	
		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);

		transform.position = target.transform.position - (rotation * offset);

		transform.LookAt (target.transform);
	}

	public void SetTarget(GameObject target){
		if (target == null) {
			return;
		}
		if (this.target != target) {
			this.target = target;
			if (this.target.tag == "Ship") {
				this.offset = new Vector3 (0, -15.0f, 10);
			}
		}
	}
}
