using UnityEngine;
using System.Collections;

public class SpearKnight : Enemy {
	Animator		animate;
	float			time;
	Vector3			pos;
	bool			reverse;
	// Use this for initialization
	void Start () {
		Init ();
		animate = GetComponent<Animator>();
		time = 0f;
		reverse = false;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		time -= Time.deltaTime;
		if (time <= 0) {
			time = 1f;
			reverse = (Vector3.Distance (pos, transform.position) < 0.1f);
			pos = transform.position;
		}
		Move ("SpearKnight", reverse);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Attack (coll.gameObject, "SpearKnight");
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		CollisionStay2D (coll);
	}

	void OnCollisionExit2D(Collision2D coll) {
		CollisionExit2D (coll);
	}
}
