using UnityEngine;
using System.Collections;

public class energyBalls : MonoBehaviour {
	Player player;
	public float speed;
	public bool beenHit;
	Vector3 velocity;

	// Use this for initialization
	void Start () {
		beenHit = false;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!beenHit) {
			velocity = player.transform.position - transform.position;
			float distance = Mathf.Sqrt (velocity.x * velocity.x + velocity.y * velocity.y);
			velocity /= distance;
			velocity *= speed;
		}
		transform.position += velocity * Time.deltaTime * speed;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.transform.tag == "Player" && !beenHit) {
			Destroy (gameObject);
			coll.gameObject.GetComponent<Player> ().DealsDamage(5);
		}
		if (coll.transform.tag == "Weapon" || coll.transform.tag == "Shoot") {
			beenHit = true;
			velocity *= -1;
			speed = 3;
		}
		if (coll.transform.tag == "Enemy" && beenHit) {
			Destroy (gameObject);
			coll.transform.GetComponent<SuperMage> ().DealsDamage (500);
		}
		if (coll.transform.tag == "Ground") {
			Destroy (gameObject);
		}
	}
}
