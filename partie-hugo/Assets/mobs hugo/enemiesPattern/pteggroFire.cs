using UnityEngine;
using System.Collections;

public class pteggroFire : MonoBehaviour {

	private Player thePlayer;
	public Vector2 speed;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<Player> ();
		speed = thePlayer.transform.position - transform.position;
		Vector2 rand = new Vector2 (speed.y, -1 * speed.x) * Random.Range(-0.1f, 0.1f);
		GetComponent<Rigidbody2D> ().velocity = speed + rand;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnBecameInvisible() {
		Destroy (transform.gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<Player> ().DealsDamage(10);
		}
		if (coll.gameObject.tag != "enemy" && coll.gameObject.tag != "bullet")
			Destroy (transform.gameObject);
	}
}
