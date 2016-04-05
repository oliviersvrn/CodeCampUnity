using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public int dmg;
	Player player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		if (Input.GetKey (KeyCode.UpArrow))
			GetComponent<Rigidbody2D> ().velocity = (new Vector2 (0, 10));
		else if (Input.GetKey (KeyCode.DownArrow) && player.ground == false)
			GetComponent<Rigidbody2D> ().velocity = (new Vector2 (0, -10));
		else
			GetComponent<Rigidbody2D> ().velocity = (new Vector2 (10 * player.view, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy")
			coll.gameObject.GetComponent<Enemy> ().DealsDamage (dmg);
		if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Weapon" && coll.gameObject.tag != "Shoot" && coll.gameObject.tag != "Platform")
			Destroy (transform.gameObject);
	}

	void OnBecameInvisible() {
		Destroy (transform.gameObject);
	}
}
