using UnityEngine;
using System.Collections;

public class witch : Enemy {

	// Use this for initialization
	public float fireRange;
	public float timeBeforeDestroy;
	public Transform shoot;
	public float maxDelay;
	void Start () {
		timeBeforeDestroy = maxDelay;
	}
	
	// Update is called once per frame
	void Update () {

		timeBeforeDestroy -= Time.deltaTime;

		if (Distance (transform.position, player.transform.position) < viewRange) {
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
		} else if (Distance (transform.position, player.transform.position) < fireRange && timeBeforeDestroy <= 0) {
				Instantiate (shoot, transform.position, transform.rotation);
			timeBeforeDestroy = maxDelay;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag == "Player") {
			coll.gameObject.GetComponent<Player> ().DealsDamage (5);
		}
	}
}
