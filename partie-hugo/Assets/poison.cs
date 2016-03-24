using UnityEngine;
using System.Collections;

public class poison : MonoBehaviour {

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
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
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
