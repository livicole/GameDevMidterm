using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour {

	CharacterController myController;
	public float walkSpeed = 5f;
	public float turnSpeed = 200f;

	// Use this for initialization
	void Start () {
		myController = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		Vector3 movement = transform.forward * walkSpeed * vertical;
		myController.Move ((movement + Physics.gravity * 2f) * Time.deltaTime);

		transform.Rotate (0f, horizontal * turnSpeed * Time.deltaTime, 0f);
	}
}
