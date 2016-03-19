using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {
	Player player;

	// Use this for initialization
	void Start() {
		player = FindObjectOfType<Player> ();
	}

	void OnGUI() {
		GUILayout.BeginArea (new Rect(10, 10, 150, 30));
		GUILayout.Label ("HP : " + player.life);
		GUILayout.EndArea ();
	}
}
