using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Life : NetworkBehaviour {

	void Start() {
		Network.sendRate = 29;
	}
	
	[ClientRpc]
	public void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// move back to zero location
			transform.position = Vector3.zero;
		}
	}
}
