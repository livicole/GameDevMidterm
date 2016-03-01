using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //NEED THIS LINE TO USE SCENEMANAGER! APPLICATION.LOADLEVEL IS BEING PHASED OUT

public class TitleScreen : MonoBehaviour {
	
	//static means this variable lives in the code, instead of on a gameobject
	//this will persist beyond a scene change
	public static bool useNightmareMode = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene (1);
		}

		if (Input.GetKeyDown (KeyCode.N)) {
			useNightmareMode = !useNightmareMode;
		}
	}
}
