using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<PlayerController> () != null) {
			Destroy (this.gameObject);
		}
	}
}
