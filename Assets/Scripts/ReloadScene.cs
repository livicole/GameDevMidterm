using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //NEED THIS LINE TO USE SCENEMANAGER! APPLICATION.LOADLEVEL IS BEING PHASED OUT
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour {

	public Image GameOver;
	public Image YouWon;


	void Update(){
		if (Input.GetMouseButtonDown(0) && GameOver.enabled || Input.GetMouseButtonDown(0) && YouWon.enabled) {
			SceneManager.LoadScene (1);
		}

		if (Input.GetMouseButtonDown (1) && GameOver.enabled || Input.GetMouseButtonDown (1) && YouWon.enabled) {
			Debug.Log ("quitting");
			Application.Quit();
		}
	}

	
	//static means this variable lives in the code, instead of on a gameobject
	//this will persist beyond a scene change
	//public static bool useNightmareMode = false;

	// Update is called once per frame
//	void Update () {
//		if (Input.GetKeyDown (KeyCode.Return)) {
//			SceneManager.LoadScene (1);
//		}
//
//		if (Input.GetKeyDown (KeyCode.N)) {
//			useNightmareMode = !useNightmareMode;
//		}
//	}

/// in another script:
/// 
/// start(){
///  if (TitleScreen.useNightmareMode == true){
/// 	healthPoints = 1; or whatever
/// }

}

