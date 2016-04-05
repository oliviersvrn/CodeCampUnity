using UnityEngine;
using System.Collections;

public class SuperMage : Enemy {
	public short direction;
	public int maxSummoned;
	int Summoned;
	public float maxTimeSummon;
	float timeSummon;
	public GameObject summon;
	float t;
	// Use this for initialization
	void Start () {
		if (generateMap.bossBeaten [1])
			Destroy (gameObject);
		Init ();
		if (direction == 0)
			direction = 1;
		timeSummon = maxTimeSummon;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		IsBoss ();
		float x = Random.Range (1.72f, 5.77f);
		float y = Random.Range (1.52f, 3.41f);
		Vector3 placement;
		placement.x = x;
		placement.y = y;
		placement.z = transform.position.z;
		transform.position += Vector3.left * direction * speed * Time.deltaTime;
		t -= Time.deltaTime;
		timeSummon -= Time.deltaTime;
		if (timeSummon <= 0) {
			timeSummon = maxTimeSummon;
			if (Summoned < maxSummoned)
				Instantiate (summon, placement, transform.rotation);
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

	void OnDestroy() {
		Camera.main.GetComponent<LifeBar> ().boss = false;
		generateMap.bossBeaten [1] = true;
	}
}
