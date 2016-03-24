using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {
	public float dmg;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player")
			coll.gameObject.GetComponent<Player> ().DealsDamage (dmg);
	}
}
