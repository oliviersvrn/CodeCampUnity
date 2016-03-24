using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPause : MonoBehaviour {
		void OnGUI(){
			if (GUI.Button (new Rect(Screen.width * .3f, Screen.height * .4f, Screen.width * .4f, Screen.height * .1f), "Main Menu")){
			SceneManager.LoadScene ("Main Menu");
		}
	}
}
