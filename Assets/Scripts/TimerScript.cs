using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TimerScript : MonoBehaviour {

	public float totalTime = 90.0f;
	public Text timerText;
	public Image gameOver;
	public Image quitGame;
	public Image timerBG;
	public bool isGameStart = false;
	public bool isTimeUp = false;
	bool hasSoundPlayed = false;
	public AudioMixer masterMixer;
	public AudioSource soundManager;
	public AudioClip hello;

	void Start(){
		gameOver.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		GameObject charController = GameObject.Find ("Player");
		CharControl charControlScript = charController.GetComponent<CharControl> ();

		string timerTextInSeconds = string.Format ("{1:00}", Mathf.Floor (totalTime / 60), totalTime % 60);

		if (isGameStart == false) {
			timerBG.enabled = false;
			timerText.enabled = false;
			masterMixer.SetFloat ("masterVol", 0f);
		} else if (isGameStart) {
			timerText.enabled = true;
			timerBG.enabled = true;
			totalTime -= Time.deltaTime;
			timerText.text = timerTextInSeconds.ToString ();
			masterMixer.SetFloat ("masterVol", 10f);
		}

		if (totalTime <= 0f) {
			charControlScript.enabled = false;
			gameOver.enabled = true;
			quitGame.enabled = true;
			totalTime = 0f;
			timerText.enabled = false;
			timerBG.enabled = false;
			masterMixer.SetFloat ("masterVol", -80f);
		}	

		if (ObjectPickUp.haveIWon == true) {
			masterMixer.SetFloat ("masterVol", -80f);

			if (hasSoundPlayed == false) {
				soundManager.PlayOneShot (hello, 1f);
				hasSoundPlayed = true;
			}
		}
	}
}
