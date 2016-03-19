using UnityEngine;
using System.Collections;

public class destroyGround : MonoBehaviour {

	public bool onGround;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = (new Vector2 (15, 3));
	}

	// Update is called once per frame
	void Update () {
		if (onGround)
			Destroy (transform.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground") {
			onGround = true;
			Destroy (coll.gameObject);
		}
	}
}