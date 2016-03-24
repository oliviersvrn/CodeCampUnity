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
		if (jumpTime <= 0 && ground) {
			jumpTime = Random.Range(3f, 6f);
			Jump ();
		}
	}

	void Jump() {
		GetComponent<Animator> ().Play ("Tricerastun_jump");
		transform.GetComponent<Rigidbody2D> ().velocity = Vector2.up * jumpPower;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		CollisionEnter2D (coll);
		if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Platform") {
			player.GetComponent<Player> ().Stun(stunTime);
			Camera.main.GetComponent<MoveCamera> ().Shake(stunTime);
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		CollisionStay2D (coll);
	}

	void OnCollisionExit2D(Collision2D coll) {
		CollisionExit2D (coll);
	}
}
