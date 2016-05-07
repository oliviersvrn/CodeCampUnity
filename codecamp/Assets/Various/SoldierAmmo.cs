using UnityEngine;
using System.Collections;

public class SoldierAmmo : MonoBehaviour {
	public float dmg;
	Player player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((player.transform.position.x < transform.position.x ? -80 : 80), 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
			player.DealsDamage (dmg, player.transform.position.x < transform.position.x);
		if (coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "Platform" && coll.gameObject.tag != "Shoot" && coll.gameObject.tag != "Weapon")
			Destroy (gameObject);
	}
}
