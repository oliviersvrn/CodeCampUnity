using UnityEngine;
using System.Collections;

public class gravityUpgrade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (generateMap.hasUpgrade [2])
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
