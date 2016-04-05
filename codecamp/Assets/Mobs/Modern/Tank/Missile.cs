using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	Player player;
	public float speed;
	public float maxAngle;
	public float dmg;
	public GameObject onde;
	float timeOnde;
	bool stop;
	Vector3 velocity;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		velocity = Vector3.left;
		timeOnde = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		stop = false;
		if (Vector3.Angle (Vector3.left, player.transform.position - transform.position) >= maxAngle / 2 || Vector3.Angle (Vector3.left, velocity) <= -1 * maxAngle / 2)
			stop = true;
		if (!stop) {
			velocity = player.transform.position - transform.position;
			float distance = Mathf.Sqrt (velocity.x * velocity.x + velocity.y * velocity.y);
			velocity /= distance;
			velocity *= speed;
		}
		transform.position += velocity * Time.deltaTime * speed;
		timeOnde -= Time.deltaTime;
		if (timeOnde <= 0) {
			Instantiate (onde, transform.position, transform.rotation);
			timeOnde = 0.2f;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player")
			player.DealsDamage (dmg);
		else if (coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "Platform")
			Destroy (gameObject);
	}
	
	void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
