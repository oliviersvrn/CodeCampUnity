using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public float dmg;
	Vector3 speed;
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		speed = player.transform.position - transform.position;
		speed *= 0.025f;
		transform.LookAt (player.transform.position, Vector3.forward);
	}

	// Update is called once per frame
	void Update () {
		transform.position += speed;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (coll.gameObject.tag == "Player")
			coll.gameObject.GetComponent<Player> ().DealsDamage (dmg, player.transform.position.x < transform.position.x);
		if (coll.gameObject.tag == "Ground") {
			Camera.main.GetComponent<MoveCamera> ().Shake(1f);
			Destroy (transform.gameObject);
		}
	}

	void OnBecameInvisible() {
		Destroy (transform.gameObject);
	}
}
