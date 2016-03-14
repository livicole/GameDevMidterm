using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
	Image youWonImage;
	Image quitGame;
	GameObject startInstructions;
	GameObject pcLight;

	void Start(){
		//Setting variables.
		startInstructions = GameObject.Find("Instructions");
		playerCube = GameObject.Find ("PlayerCube").GetComponent<Transform>();
		playerPos = GameObject.Find ("Player").GetComponent<Transform> ();
		pickUpImage = GameObject.Find ("ObjectInstructions").GetComponent<Image>();
		youWonImage = GameObject.Find ("YouWon").GetComponent<Image> ();
		quitGame = GameObject.Find ("Quit").GetComponent<Image> ();
		pickUpObject = this.gameObject.GetComponent<Transform> ();
		pcLight = GameObject.Find ("PCHalo");
		thisColl = GetComponent<Collider> ();
		pickUpImage.enabled = false;
		youWonImage.enabled = false;
		quitGame.enabled = false;
		amICurrentlyHoldingSomething = false;
		haveIWon = false;
		myAudio = GameObject.Find ("SoundManager").GetComponent<AudioSource> ();
	}

	void Update(){
		
		Vector3 velocityByDistance = (playerCube.position - pickUpObject.position) * 8f;
		GameObject gameTimer = GameObject.Find ("Timer");
		TimerScript timeScript = gameTimer.GetComponent<TimerScript> ();

		//Pick up object functions.
		if (isPickedUp == true && pickUpObject.gameObject.tag != "Phone" && timeScript.totalTime > 0) {

			thisColl.attachedRigidbody.useGravity = false;
			this.gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			pickUpImage.enabled = true;

			//Pick up object with left click.
			if (Input.GetMouseButtonDown (0) && Time.time > timePickedUp + 0.1f) {
				isPickedUp = false;
				amICurrentlyHoldingSomething = false;
				pickUpImage.enabled = false;
				//Drop object with right click.
			} else if (Input.GetMouseButtonDown (1)) {
				myAudio.PlayOneShot (putAwaySound, 1f);
				Destroy (this.gameObject);
				amICurrentlyHoldingSomething = false;
				pickUpImage.enabled = false;
			}
			//Bring object to player pos.
			if ((playerCube.position - pickUpObject.position).magnitude > moveDistance) {
				pickUpObject.GetComponent<Rigidbody> ().velocity = velocityByDistance;
			} else {
				pickUpObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}

			if (pickUpObject.gameObject.tag == "PC") {
				this.gameObject.transform.localEulerAngles = new Vector3 (0f, 90f, 0f);
				pcLight.SetActive (false);
			}

		} else if (isPickedUp == false && pickUpObject.gameObject.tag != "Phone") {
			thisColl.attachedRigidbody.useGravity = true;

		} else if (isPickedUp == true && pickUpObject.gameObject.tag == "Phone") {
			//End game if object is the phone.
			thisColl.attachedRigidbody.useGravity = false;
			this.gameObject.transform.localEulerAngles = new Vector3 (0f, 180f, 180f);
			if ((playerCube.position - pickUpObject.position).magnitude > moveDistance) {
				pickUpObject.GetComponent<Rigidbody> ().velocity = velocityByDistance;
			} else {
				pickUpObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}

			if (haveIWon == false) {
				Invoke ("YouWon", 0.1f);
			}
		}
	}


	void OnMouseOver(){
		if (!startInstructions.activeInHierarchy) {		
			if (Input.GetMouseButtonDown (0) && Vector3.Distance (playerPos.transform.position, this.transform.position) < canPickUpDistance && amICurrentlyHoldingSomething == false) {
				isPickedUp = true;
				amICurrentlyHoldingSomething = true;
				timePickedUp = Time.time;
			}
		}
	}

	void YouWon(){
		GameObject gameTimer = GameObject.Find ("Timer");
		TimerScript timeScript = gameTimer.GetComponent<TimerScript> ();

		haveIWon = true;
		pickUpImage.enabled = false;
		youWonImage.enabled = true;
		quitGame.enabled = true;
		timeScript.isGameStart = false;
	}
}
