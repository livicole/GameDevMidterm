using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class TimerScript : MonoBehaviour {

	public float totalTime = 90.0f;
	public Text timerText;
	public Text gameOver;
	public Text tryAgain;
	public bool isGameStart = false;
	public bool isTimeUp = false;
	public AudioMixer masterMixer;

	void Start(){
		gameOver.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		GameObject charController = GameObject.Find ("Player");
		CharControl charControlScript = charController.GetComponent<CharControl> ();


		if (isGameStart == false) {
			timerText.enabled = false;
			masterMixer.SetFloat ("masterVol", 20f);
		} else if (isGameStart) {
			timerText.enabled = true;
			totalTime -= Time.deltaTime;
			timerText.text = totalTime.ToString();
			masterMixer.SetFloat ("masterVol", 20f);
		}

		if (totalTime <= 0f) {
			charControlScript.enabled = false;
			gameOver.enabled = true;
			totalTime = 0f;
			timerText.enabled = false;
			tryAgain.enabled = true;
			masterMixer.SetFloat ("masterVol", -80f);
		}
	
	}
}
