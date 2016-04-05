using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour {

	public float timeBeforeDestroy;
	Player player;
	public float speed;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		timeBeforeDestroy = 3;
	}
	
	// Update is called once per frame
	void Update () {
		timeBeforeDestroy -= Time.deltaTime;
		Vector3 velocity = player.transform.position - transform.position;
		float distance = Mathf.Sqrt (velocity.x * velocity.x + velocity.y * velocity.y);
		velocity /= distance;
		velocity *= speed;
		transform.position += velocity * Time.deltaTime * speed;
		if (timeBeforeDestroy < 0)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.transform.tag == "Player") {
			Destroy (gameObject);
			coll.gameObject.GetComponent<Player> ().Poison(5f, 15f);
		}
	}
}
