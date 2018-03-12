using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookAtCamera : MonoBehaviour {
	private GameObject target;
	private Vector3 offset;
	float angleX = 0;

	void Start() {
		offset = new Vector3(0,-2.0f,3);
	}

	void LateUpdate() {
		if (target == null) {
			return;
		}
	
		float desiredAngleY = target.transform.eulerAngles.y;
		//float desiredAngleX = target.transform.eulerAngles.x;
		var desiredAngleX = angleX - Input.GetAxis ("Mouse Y") * 2f;	
		if(desiredAngleX < 54f && desiredAngleX > -90f){ //wtf is this values?  probably depends resulting offset position angle
			angleX = desiredAngleX;	
		}

		Quaternion rotation = Quaternion.Euler (desiredAngleX, desiredAngleY, 0);

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
				this.offset = new Vector3 (0, -25.0f, 20);
			}
			if (this.target.tag == "Player") {
				this.offset = new Vector3(0,-2.0f,3);
			}
		}
	}
}
