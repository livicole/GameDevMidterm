using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	public Text start;
	
	void OnMouseEnter(){
		start.color = new Color (0.937f, 0.0705f, 0.2549f);
	}

	void OnMouseExit(){
		start.color = new Color (0.1294f, 0.0862f, 0.0862f);
	}

	void OnMouseDown(){
		SceneManager.LoadScene (1);
	}
}
