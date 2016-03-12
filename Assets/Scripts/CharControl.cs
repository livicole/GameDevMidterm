using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour {

	CharacterController myController;
	public float walkSpeed = 5f;
	public float turnSpeed = 150f;
	public float hitForce = 2f;

	// Use this for initialization
	void Start () {
		myController = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		float vertical = Input.GetAxis ("Vertical");
		float horizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = transform.forward * walkSpeed * vertical;
		Vector3 sideMovement = transform.right * walkSpeed * horizontal;
		myController.Move(((movement + sideMovement) + Physics.gravity * 2f) * Time.deltaTime);

	}

	void OnControllerColliderHit(ControllerColliderHit hitObject){
		Rigidbody bodyRB = hitObject.collider.attachedRigidbody;
		if (bodyRB == null || bodyRB.isKinematic) {
			return;
		}

//		Vector3 pushDirection = new Vector3 (hitObject.moveDirection.x, 0f, hitObject.moveDirection.z);
//		bodyRB.velocity = pushDirection * hitForce;
	}
}
