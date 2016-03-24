using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
//	public float guiPlacementY;
//
//	public float guiPlacementX;

	void OnGUI(){

		///Fait apparaitre les boutons au lancemen
		if (GUI.Button (new Rect(Screen.width * .3f, Screen.height * .4f, Screen.width * .4f, Screen.height * .1f), "Play")){
			SceneManager.LoadScene ("Scene test");
		}
		if (GUI.Button (new Rect(Screen.width * .3f, Screen.height * .6f, Screen.width * .4f, Screen.height * .1f), "Quit")){
			Application.Quit();
		}
	}
}
