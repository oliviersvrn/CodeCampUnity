using UnityEngine;
using System.Collections;

public class Rushosaure : Enemy {
	private Animator animate;
	// Use this for initialization
	void Start () {
		Init ();
		animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		Move ("Rushosaure");
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Attack (coll.gameObject, "Rushosaure");
		}
	}
}
