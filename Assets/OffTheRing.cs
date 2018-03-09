using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffTheRing : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Life>() != null) {
			Life life = other.gameObject.GetComponent<Life> () as Life;
			life.RpcRespawn();
		}
	}
}
