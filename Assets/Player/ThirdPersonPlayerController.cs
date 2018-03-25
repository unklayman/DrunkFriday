using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

[RequireComponent (typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(Characteristics))]
public class ThirdPersonPlayerController : NetworkBehaviour, IPlayer
{
	private ThirdPersonCharacter m_Character;
	// A reference to the ThirdPersonCharacter on the object
	private Vector3 m_Move;
	private bool m_Jump;
	// the world-relative desired move direction, calculated from the camForward and user input.
	private float maxDistance = 1.5f;

	public IShip Ship { get; set; }

	public Camera Camera { get; set; }

	private ICharacteristics chars;

	private void Start ()
	{
		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<ThirdPersonCharacter> ();
		chars = GetComponent<Characteristics> ();
	}


	private void Update ()
	{
		if (!m_Jump) {
			m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
		}
	}


	// Fixed update is called in sync with physics
	private void FixedUpdate ()
	{
		// read inputs
		float h = CrossPlatformInputManager.GetAxis ("Mouse X");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");
		bool interact = CrossPlatformInputManager.GetButtonDown (KeyCode.E.ToString());

		Interact ();

		if (v < 0)
			v = 0;
		bool crouch = Input.GetKey (KeyCode.LeftControl);
		m_Move = v * gameObject.transform.forward + h * gameObject.transform.right;

		// pass all parameters to the character control script
		m_Character.Move (m_Move, crouch, m_Jump);
		m_Jump = false;
	}

	void Interact ()
	{
		var lm = LayerMask.GetMask ("Interactable");
		RaycastHit hit;
		if (Physics.Raycast (transform.position, gameObject.transform.TransformDirection (Vector3.forward), out hit, maxDistance, lm)) {

			var target = hit.collider.gameObject;
			if (target.name.Equals ("ShipControl")) {
				GameController.PlayerShipInteraction (this, target.GetComponentInParent<ShipController> (), InteractionType.PlayerTakesShipControl);
			}
			if (target.name.Equals ("GunControl")) {
				GameController.PlayerShipInteraction (this, target.GetComponentInParent<ShipController> (), InteractionType.PlayerTakesShipGunsControl);
			}
		}
	}

	#region ICharacteristics implementation

	public void DoDamage (int amount)
	{
		chars.DoDamage (amount);
	}

	public void Destroy ()
	{
		chars.Destroy ();
	}

	#endregion
}
