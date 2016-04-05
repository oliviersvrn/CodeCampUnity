using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (GameObject.FindGameObjectWithTag ("Music").gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return))
			SceneManager.LoadScene ("Menu");
	}
}
