using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {
	public string level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return))
			SceneManager.LoadScene (level);;
	}
}
