using UnityEngine;
using System.Collections;

public class ObjectPickUp : MonoBehaviour {

	public Transform playerCube;
	public Transform playerPos;
	Transform pickUpObject;
	float canPickUpDistance = 10f;
	float moveDistance = 0.05f;
	bool isPickedUp = false;
	Collider thisColl;
	Vector3 originalScale;

	void Start(){
		pickUpObject = GetComponent<Transform> ();
		thisColl = GetComponent<Collider> ();
		originalScale = this.gameObject.transform.localScale;
	}

	void Update(){

		Vector3 velocityByDistance = (playerCube.position - pickUpObject.position) * 8f;

		if (isPickedUp) {

			if (Input.GetMouseButtonDown (0)) {
				isPickedUp = false;
			}

			if ((playerCube.position - pickUpObject.position).magnitude > moveDistance) {
				pickUpObject.GetComponent<Rigidbody> ().velocity = velocityByDistance;
			} else {
				pickUpObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}

			thisColl.attachedRigidbody.useGravity = false;
			this.gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			//this.gameObject.transform.localScale = originalScale;

		} else {
			thisColl.attachedRigidbody.useGravity = true;
		}
	}


	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0) && Vector3.Distance (playerPos.transform.position, this.transform.position) < canPickUpDistance) {
			isPickedUp = !isPickedUp;
		}
	}
}
