using UnityEngine;
using System.Collections;

public class destroyGround : MonoBehaviour {

	public Player player;
	public float explosionRange;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
		GetComponent<Rigidbody2D> ().velocity = (new Vector2 ((player.transform.position.x - transform.position.x) * 1.2f, 3.3f));
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "Shoot" && coll.gameObject.tag != "Platform") {
			if (Enemy.Distance (player.transform.position, transform.position) < explosionRange)
				player.DealsDamage (10, player.transform.position.x < transform.position.x);
			Destroy (gameObject);
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere (transform.position, explosionRange);
	}
}