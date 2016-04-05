using UnityEngine;
using System.Collections;

public class BrokenRobot : Enemy {
	float countDown;
	float timeLeft;
	bool activate;
	// Use this for initialization
	void Start () {
		Init ();
		activate = false;
		countDown = Random.Range (2, 5);
		timeLeft = countDown;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		if (Distance (player.transform.position, transform.position) <= viewRange)
			activate = true;
		if (activate)
			timeLeft -= Time.deltaTime;
		if (timeLeft <= 0) {
			if (Distance (player.transform.position, transform.position) <= 0.7f)
				player.GetComponent<Player> ().DealsDamage (dmg, player.transform.position.x < transform.position.x);
			Destroy (gameObject);
		}
		Move ("BrokenRobot");
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
