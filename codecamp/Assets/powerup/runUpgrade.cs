using UnityEngine;
using System.Collections;

public class runUpgrade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (generateMap.hasUpgrade [0])
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
