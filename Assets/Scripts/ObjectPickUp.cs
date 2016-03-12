using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ObjectPickUp : MonoBehaviour {

	Transform pickUpObject;
	Transform playerCube;
	Transform playerPos;
	float canPickUpDistance = 15f;
	float moveDistance = 0.05f;
	float timePickedUp;
	Collider thisColl;
	bool isPickedUp = false;
	public static bool amICurrentlyHoldingSomething = false;
	public static bool haveIWon = false;
	AudioSource myAudio;
	public AudioClip putAwaySound;
	Image pickUpImage;
	Text winText;
	Text playAgainText;

	void Start(){
		//Setting variables.
		playerCube = GameObject.Find ("PlayerCube").GetComponent<Transform>();
		playerPos = GameObject.Find ("Player").GetComponent<Transform> ();
		pickUpImage = GameObject.Find ("ObjectInstructions").GetComponent<Image>();
		winText = GameObject.Find ("YouWin").GetComponent<Text>();
		playAgainText = GameObject.Find ("PlayAgain").GetComponent<Text>();
		pickUpObject = this.gameObject.GetComponent<Transform> ();
		thisColl = GetComponent<Collider> ();
		pickUpImage.enabled = false;
		winText.enabled = false;
		playAgainText.enabled = false;
		amICurrentlyHoldingSomething = false;
		haveIWon = false;
		myAudio = GameObject.Find ("SoundManager").GetComponent<AudioSource> ();
	}

	void Update(){
		
		Vector3 velocityByDistance = (playerCube.position - pickUpObject.position) * 8f;

		//Pick up object functions.
		if (isPickedUp == true && pickUpObject.gameObject.tag != "Phone") {

			thisColl.attachedRigidbody.useGravity = false;
			this.gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			pickUpImage.enabled = true;

			//Pick up object with left click.
			if (Input.GetMouseButtonDown (0) && Time.time > timePickedUp + 0.1f) {
				isPickedUp = false;
				amICurrentlyHoldingSomething = false;
				pickUpImage.enabled = false;
				playAgainText.enabled = false;
				//Drop object with right click.
			} else if (Input.GetMouseButtonDown (1)) {
				myAudio.PlayOneShot (putAwaySound, 1f);
				Destroy (this.gameObject);
				amICurrentlyHoldingSomething = false;
				pickUpImage.enabled = false;
				playAgainText.enabled = false;
			}
			//Bring object to player pos.
			if ((playerCube.position - pickUpObject.position).magnitude > moveDistance) {
				pickUpObject.GetComponent<Rigidbody> ().velocity = velocityByDistance;
			} else {
				pickUpObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}

		} else if (isPickedUp == false && pickUpObject.gameObject.tag != "Phone") {
			thisColl.attachedRigidbody.useGravity = true;
		} else if (isPickedUp == true && pickUpObject.gameObject.tag == "Phone") {
			//End game if object is the phone.
			thisColl.attachedRigidbody.useGravity = false;
			this.gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			if ((playerCube.position - pickUpObject.position).magnitude > moveDistance) {
				pickUpObject.GetComponent<Rigidbody> ().velocity = velocityByDistance;
			} else {
				pickUpObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}

			if (haveIWon == false) {
				YouWon ();
			}
		}
	}


	void OnMouseOver(){
		
		if (Input.GetMouseButtonDown (0) && Vector3.Distance (playerPos.transform.position, this.transform.position) < canPickUpDistance && amICurrentlyHoldingSomething == false) {
			isPickedUp = true;
			amICurrentlyHoldingSomething = true;
			timePickedUp = Time.time;
		}
	}

	void YouWon(){
		GameObject gameTimer = GameObject.Find ("Timer");
		TimerScript timeScript = gameTimer.GetComponent<TimerScript> ();

		haveIWon = true;
		pickUpImage.enabled = false;
		winText.enabled = true;
		playAgainText.enabled = true;
		timeScript.isGameStart = false;
		this.gameObject.transform.localEulerAngles = new Vector3 (0f, 180f, 180f);
	}
}
