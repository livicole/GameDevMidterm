using UnityEngine;
using System.Collections;

public class ObjectPickUp : MonoBehaviour {

	public Transform player;
	public Transform playerPos;
	float distance = 10f;
	bool isPickedUp = false;
	Collider thisColl;
	Vector3 originalScale;

	void Start(){
		thisColl = GetComponent<Collider> ();
		originalScale = this.gameObject.transform.localScale;
	}

	void Update(){
		if (isPickedUp) {
			thisColl.attachedRigidbody.useGravity = false;
			//thisColl.material = null;
			this.gameObject.transform.SetParent (player.transform);
			this.gameObject.transform.position = player.transform.position;
			this.gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			this.gameObject.transform.localScale = originalScale;
		} else {
			thisColl.attachedRigidbody.useGravity = true;
			//thisColl.material = new PhysicMaterial ("Bouncy");
			player.transform.DetachChildren ();
		}
	}


	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0) && Vector3.Distance(playerPos.transform.position, this.transform.position) < distance) {
			isPickedUp = !isPickedUp;
		}
	}
}
