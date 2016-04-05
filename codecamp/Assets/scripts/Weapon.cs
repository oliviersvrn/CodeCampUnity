using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float dmg;
	public bool attack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy" && attack)
			coll.gameObject.GetComponent<Enemy> ().DealsDamage (dmg, 2);
	}
}
