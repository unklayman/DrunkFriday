﻿using System;
using UnityEngine;

public class PlayerCamera : BaseCamera
{
	private static readonly Vector3 offset = new Vector3 (0, -2.0f, 3);

	void LateUpdate ()
	{
		if (Target == null) {
			return;
		}
					
		var desiredAngleX = angleX - Input.GetAxis ("Mouse Y") * Sensitivity;	
		if (desiredAngleX < 54f && desiredAngleX > -90f) { //wtf is this values?  probably depends resulting offset position angle
			angleX = desiredAngleX;	
		}
		angleY += Input.GetAxis ("Mouse X") * Sensitivity;

		Quaternion rotation = Quaternion.Euler (desiredAngleX,angleY, 0);

		transform.position = Target.transform.position - (rotation * offset);
		transform.LookAt (Target.transform);
	}
}
