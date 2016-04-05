using UnityEngine;
using System.Collections;

public class cataFire : Enemy {

	public float maxDelayShot;
	public float actualDelay;
	public Transform shoot;

	// Use this for initialization
	void Start () {
		actualDelay = maxDelayShot;
	}
	
	// Update is called once per frame
	void Update () {
		actualDelay -= Time.deltaTime;
		if (Distance (transform.position, player.transform.position) < viewRange) {
			if (actualDelay <= 0) {
				GetComponent<Animator> ().Play ("catapulte_attack");
				Instantiate (shoot, transform.position + Vector3.up * 0.5f * transform.localScale.y, transform.rotation);
				actualDelay = maxDelayShot;
				GetComponent<Animator> ().Play ("catapulte_stop");
			}
		}
		Vector3 scale = transform.localScale;
		if (transform.position.x < player.transform.position.x) {
			if (scale.x < 0)
				scale.x *= -1;
			}
			else {
				if (scale.x > 0)
					scale.x *= -1;
			}
			transform.localScale = scale;	
		}
}
