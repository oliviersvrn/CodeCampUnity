using UnityEngine;
using System.Collections;

public class Tank : Enemy {
	public float maxTimeToShoot;
	float timeToShoot;
	public float maxTimeToFly;
	float timeToFly;
	public GameObject missile;
	bool fly;
	Vector3 flySpeed;
	// Use this for initialization
	void Start () {
		Init ();
		timeToFly = maxTimeToFly;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		timeToShoot -= Time.deltaTime;
		timeToFly -= Time.deltaTime;

		if (timeToShoot <= 0) {
			timeToShoot = maxTimeToShoot;
			Instantiate (missile, transform.position, transform.rotation);
		}

		if (timeToFly <= 0) {
			timeToFly = maxTimeToFly;
			flySpeed = player.transform.position - transform.position + Vector3.up * 3.5f;
			fly = true;
		}

		if (fly) {
			transform.position += flySpeed * Time.deltaTime * speed;
			GetComponent<Rigidbody2D> ().gravityScale = 0f;
			if (transform.position.x - player.transform.position.x < 0.5f &&
				transform.position.x - player.transform.position.x > -0.5f) {
				fly = false;
				GetComponent<Rigidbody2D> ().gravityScale = 0.2f;
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		CollisionStay2D (coll);
	}

	void OnCollisionExit2D(Collision2D coll) {
		CollisionExit2D (coll);
	}
}
