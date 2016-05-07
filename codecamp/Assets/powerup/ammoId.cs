﻿using UnityEngine;
using System.Collections;

public class ammoId : MonoBehaviour {

	public string id;

	// Use this for initialization
	void Start () {
		id = generateMap.posX + "" + generateMap.posY;
		for (int i = 0; i < 10; i++){
			if (generateMap.ammoTaken[i, 0] == id && generateMap.ammoTaken[i, 1] == "ok")
				Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "Player") {
			for (int i = 0; i < 10; i++){
				if (generateMap.ammoTaken[i, 0] == id)
					generateMap.ammoTaken[i, 1] = "ok";
			}
			Destroy (gameObject);
		}
	}
}