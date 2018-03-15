using System;
using UnityEngine;

public abstract class BaseCamera : MonoBehaviour
{
	private GameObject target;
	protected float angleX = 0;
	protected float angleY = 0;

	public float Sensitivity = 2f;

	public GameObject Target {
		get
		{
			return target;
		}
		set
		{
			if (value == null || value == target) {
				return;
			}

			target = value;
		}
	}
}
