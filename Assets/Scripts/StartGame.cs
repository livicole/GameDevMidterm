using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public GameObject phonePrefab;
	public Vector3[] phonePositions;


	// Use this for initialization
	void Start () {
		
		int randomArrayChoose = Random.Range (0, 7);
		Quaternion phoneRotate = Quaternion.Euler (0f, 0f, 180f);
		Instantiate (phonePrefab, phonePositions[randomArrayChoose], phoneRotate);

		GameObject charController = GameObject.Find ("Player");
		CharControl charControlScript = charController.GetComponent<CharControl> ();
		charControlScript.enabled = false;
	}
	
	void Update(){
		GameObject charController = GameObject.Find ("Player");
		CharControl charControlScript = charController.GetComponent<CharControl> ();
		GameObject gameTimer = GameObject.Find ("Timer");
		TimerScript timeScript = gameTimer.GetComponent<TimerScript> ();

		if (Input.GetMouseButtonDown(0)) {
			this.gameObject.SetActive (false);
			charControlScript.enabled = true;
			timeScript.isGameStart = true;
		}

	}
}
