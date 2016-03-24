using UnityEngine;
using System.Collections;

public class SuperMage : Enemy {
	public short direction;
	public int maxSummoned;
	int Summoned;
	public float maxTimeSummon;
	float timeSummon;
	public GameObject summon;
	public float maxTimeFire;
	public GameObject fire;
	float timeFire;
	public float t;
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
		float x = Random.Range (0.11f, 4.70f);
		float y = Random.Range (0.25f, 2.65f);
		Vector3 placement;
		placement.x = x;
		placement.y = y;
		placement.z = 0;
		Up ();
//		IsBoss ();
		transform.position += Vector3.left * direction * speed * Time.deltaTime;
		t -= Time.deltaTime;
		timeSummon -= Time.deltaTime;
		timeFire -= Time.deltaTime;
		if (timeSummon <= 0) {
			timeSummon = maxTimeSummon;
			if (Summoned < maxSummoned)
				Instantiate (summon, placement, transform.rotation);
		}
		if (timeFire <= 0) {
			timeFire = maxTimeFire;
//			Instantiate (fire, transform.position, transform.rotation);
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
		if (coll.transform.tag == "Player") {
			coll.gameObject.GetComponent<Player> ().Poison (5f, 3f);
		}
	}
}
