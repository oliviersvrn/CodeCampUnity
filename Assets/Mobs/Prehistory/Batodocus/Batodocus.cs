using UnityEngine;
using System.Collections;

public class Batodocus : Enemy {
	float shootTime;
	public float maxShootTime;
	public Transform shoot;
	public float delay;
	// Use this for initialization
	void Start () {
		Init ();
		shootTime = 0;
		delay *= maxShootTime;
		shootTime = delay;
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		shootTime -= Time.deltaTime;

		if (shootTime <= 0)
			shootAmmo ();
	}

	void shootAmmo() {
		shootTime = maxShootTime;
		Instantiate (shoot, transform.position, transform.rotation);
	}
}
