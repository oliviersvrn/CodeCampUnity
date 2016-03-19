using UnityEngine;
using System.Collections;

public class Tricerastun : Enemy {
	float jumpTime;
	public float jumpPower;
	public float stunTime;
	// Use this for initialization
	void Start () {
		Init ();
		jumpTime = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		jumpTime -= Time.deltaTime;
		if (jumpTime <= 0) {
			jumpTime = Random.Range(3f, 6f);
			Jump ();
		}
	}

	void Jump() {
		transform.GetComponent<Rigidbody2D> ().velocity = Vector2.up * jumpPower;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			player.GetComponent<Player> ().Stun(stunTime);
			Camera.main.GetComponent<MoveCamera> ().Shake(stunTime);
		}
	}
}
