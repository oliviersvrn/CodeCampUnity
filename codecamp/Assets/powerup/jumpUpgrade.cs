using UnityEngine;
using System.Collections;

public class jumpUpgrade : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (generateMap.hasUpgrade [1])
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
