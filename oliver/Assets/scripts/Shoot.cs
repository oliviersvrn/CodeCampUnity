using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public int dmg;
	public Player player;
	// Use this for initialization
	void Start () {
		dmg = 5;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		if (Input.GetKey (KeyCode.UpArrow))
			GetComponent<Rigidbody2D> ().velocity = (new Vector2 (0, 20));
		else if (Input.GetKey (KeyCode.DownArrow) && player.ground == false)
			GetComponent<Rigidbody2D> ().velocity = (new Vector2 (0, -20));
		else
			GetComponent<Rigidbody2D> ().velocity = (new Vector2 (20 * player.view, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy")
			coll.gameObject.GetComponent<Enemy> ().DealsDamage(dmg);
		if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Weapon")
			Destroy (transform.gameObject);
	}

	void OnBecameInvisible() {
		Destroy (transform.gameObject);
	}
}
