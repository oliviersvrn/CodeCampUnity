using UnityEngine;
using System.Collections;

public class SpearKnight : Enemy {
	private Animator animate;
	// Use this for initialization
	void Start () {
		Init ();
		animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		Move ("SpearKnight");
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Attack (coll.gameObject, "SpearKnight");
		}
	}
}
