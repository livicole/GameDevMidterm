using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ObjectPickUp : MonoBehaviour {

	Transform pickUpObject;
	float canPickUpDistance = 15f;
	float moveDistance = 0.05f;
	Collider thisColl;
	bool isPickedUp = false;
	public static bool amICurrentlyHoldingSomething = false;
	float timePickedUp;
	AudioSource myAudio;
	AudioClip putAwaySound;
	Transform playerCube;
	Transform playerPos;
	Image pickUpImage;
	Text winText;
	Text playAgainText;

	void Start(){
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
		myAudio = GameObject.Find ("SoundManager").GetComponent<AudioSource> ();
		putAwaySound = GameObject.Find ("SoundManager").GetComponent<AudioClip> ();
	}

	void Update(){

		GameObject gameTimer = GameObject.Find ("Timer");
		TimerScript timeScript = gameTimer.GetComponent<TimerScript> ();
		Vector3 velocityByDistance = (playerCube.position - pickUpObject.position) * 8f;

		if (isPickedUp == true) {

			thisColl.attachedRigidbody.useGravity = false;
			this.gameObject.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
			pickUpImage.enabled = true;


			if (Input.GetMouseButtonDown(0) && Time.time > timePickedUp + 0.1f) {
				isPickedUp = false;
				amICurrentlyHoldingSomething = false;
				pickUpImage.enabled = false;
				playAgainText.enabled = false;

			} else if(Input.GetMouseButtonDown(1)){
				myAudio.PlayOneShot (putAwaySound, 1f);
				Destroy (this.gameObject);
				amICurrentlyHoldingSomething = false;
				pickUpImage.enabled = false;
				playAgainText.enabled = false;
			}

			if ((playerCube.position - pickUpObject.position).magnitude > moveDistance) {
				pickUpObject.GetComponent<Rigidbody> ().velocity = velocityByDistance;
			} else {
				pickUpObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			}

			if (pickUpObject.gameObject.tag == "Phone") {
				pickUpImage.enabled = false;
				winText.enabled = true;
				playAgainText.enabled = true;
				timeScript.isGameStart = false;
				this.gameObject.transform.localEulerAngles = new Vector3 (0f, 180f, 180f);
				timeScript.masterMixer.SetFloat ("masterVol", -80f);
			}

		} else {
			thisColl.attachedRigidbody.useGravity = true;
		}
	}


	void OnMouseOver(){
		
		if (Input.GetMouseButtonDown (0) && Vector3.Distance (playerPos.transform.position, this.transform.position) < canPickUpDistance && amICurrentlyHoldingSomething == false) {
			isPickedUp = true;
			amICurrentlyHoldingSomething = true;
			timePickedUp = Time.time;
		}
	}
}
