using UnityEngine;
using System.Collections;

public class soldier : Enemy {
	public float maxTimeShoot;
	float timeShoot;
	public GameObject shoot;
	float t;
	float maxT;
	// Use this for initialization
	void Start () {
		Init ();
		maxT = 0.3f;
		timeShoot = 0f;
	}

	// Update is called once per frame
	void Update () {
		Up ();
		if (Distance (transform.position, player.transform.position) < viewRange) {
			timeShoot -= Time.deltaTime;
			if (timeShoot <= 0)
				timeShoot = maxTimeShoot;
			t -= Time.deltaTime;
			if (t <= 0 && timeShoot < maxTimeShoot / 2) {
				Instantiate (shoot, transform.position, transform.rotation);
				t = maxT;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		CollisionEnter2D (coll);
	}

	void OnCollisionStay2D(Collision2D coll) {
		CollisionStay2D (coll);
	}

	void OnCollisionExit2D(Collision2D coll) {
		CollisionExit2D (coll);
	}
}
