using UnityEngine;
using System.Collections;

public class Witch : Enemy {

	// Use this for initialization
	public float fireRange;
	float timeBeforeDestroy;
	public Transform shoot;
	public float maxDelay;
	void Start () {
		Init ();
		timeBeforeDestroy = maxDelay;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		timeBeforeDestroy -= Time.deltaTime;

		if (Distance (transform.position, player.transform.position) < viewRange) {
			Vector3 velocity = player.transform.position - transform.position;
			float distance = Mathf.Sqrt (velocity.x * velocity.x + velocity.y * velocity.y);
			velocity /= distance;
			velocity *= speed;
			transform.position += velocity * Time.deltaTime * speed;
		} else if (Distance (transform.position, player.transform.position) < fireRange && timeBeforeDestroy <= 0) {
			Shoot ();
			timeBeforeDestroy = maxDelay;
		}
	}

	void Shoot() {
		GetComponent<Animator> ().Play ("Witch_attack");
		Instantiate (shoot, transform.position, transform.rotation);
	}

	void OnCollisionEnter2D(Collision2D coll){
		CollisionEnter2D (coll);
	}

	void OnCollisionStay2D(Collision2D coll){
		CollisionStay2D (coll);
	}
		
	void OnCollisionExit2D(Collision2D coll){
		CollisionExit2D (coll);
	}
}
