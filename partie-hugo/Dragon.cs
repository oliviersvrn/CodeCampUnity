using UnityEngine;
using System.Collections;

public class Dragon : Enemy {
	public short direction;
	public int maxSummoned;
	int Summoned;
	public float maxTimeSummon;
	float timeSummon;
	public GameObject summon;
	public float maxTimeFire;
	public GameObject fire;
	float timeFire;
	float t;
	// Use this for initialization
	void Start () {
		Init ();
		if (direction == 0)
			direction = 1;
		timeSummon = maxTimeSummon;
		timeFire = maxTimeFire;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		IsBoss ();
		transform.position += Vector3.left * direction * speed * Time.deltaTime;
		t -= Time.deltaTime;
		timeSummon -= Time.deltaTime;
		timeFire -= Time.deltaTime;
		if (timeSummon <= 0) {
			timeSummon = maxTimeSummon;
			if (Summoned < maxSummoned)
				Instantiate (summon, transform.position, transform.rotation);
		}
		if (timeFire <= 0) {
			timeFire = maxTimeFire;
			Instantiate (fire, transform.position, transform.rotation);
		}
		Summoned = GameObject.FindGameObjectsWithTag ("Enemy").Length - 1;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground" && t <= 0) {
			direction *= -1;
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			t = 1;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		CollisionStay2D (coll);
	}
}
