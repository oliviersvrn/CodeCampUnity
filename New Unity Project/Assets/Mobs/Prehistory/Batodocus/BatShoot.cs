using UnityEngine;
using System.Collections;

public class BatShoot : MonoBehaviour {
	public float dmg;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBecameInvisible() {
		Destroy (transform.gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
			coll.gameObject.GetComponent<Player> ().DealsDamage(dmg);
		if (coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "Weapon" && coll.gameObject.tag != "Shoot")
			Destroy (transform.gameObject);
	}
}
