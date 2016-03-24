using UnityEngine;
using System.Collections;

public class pteggroPattern : Enemy {

	public short id;
	private Animator animate;
//	private Player thePlayer;
//	public float moveSpeed;
	public float playerRange;
	public bool playerInRange;
	public float fireRange;
	public bool playerInFireRange;
//	public LayerMask playerLayer;
	public Transform theNest;
	public Transform shoot;
	public float shotSpeed;
	private float timeToShot;
//	public float hp;
//	public float maxHp;
//	public bool onGround;
	// Use this for initialization
	void Start () {
		animate = GetComponent<Animator>();
//		maxHp = 50;
//		hp = maxHp;
		Init();
		timeToShot = shotSpeed;
//		thePlayer = FindObjectOfType<Player> ();
		foreach (GameObject nests in GameObject.FindGameObjectsWithTag ("nest")) {
			if (nests.GetComponent<nest> ().id == id)
				theNest = nests.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Up ();
		if (transform.GetComponent<Rigidbody2D> ().velocity.x > 0)
			transform.localScale = new Vector3(3.289652f, 2.2308f, 0);
		if (transform.GetComponent<Rigidbody2D> ().velocity.x < 0)
			transform.localScale = new Vector3(-3.289652f, 2.2308f, 0);
//		if (hp <= 0)
//			Destroy (transform.gameObject);
		playerInRange = Distance(transform.position, player.transform.position) < playerRange;
		playerInFireRange = Distance(transform.position, player.transform.position) < fireRange;
		if (playerInFireRange && !playerInRange)
			fireOnPlayer ();
		if (playerInRange) {
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, speed * Time.deltaTime);
			animate.Play ("pteggro_flying");
		}
		else if (!ground && transform.GetComponent<Rigidbody2D> ().velocity == Vector2.zero)
			animate.Play ("pteggro_engage");
		if (!playerInRange && !playerInFireRange) {
			transform.position = Vector3.MoveTowards (transform.position, theNest.position, speed * Time.deltaTime);
		}
		timeToShot -= Time.deltaTime;
	}

	void fireOnPlayer(){
		if (timeToShot <= 0) {
			Instantiate (shoot, transform.position, transform.rotation);
			timeToShot = shotSpeed;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		CollisionEnter2D (coll);
//		if (coll.gameObject.tag == "ground")
//			onGround = true;
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		CollisionExit2D (coll);
//		if (coll.gameObject.tag == "ground")
//			onGround = false;
	}

//	void OnCollisionStay2D(Collision2D coll){
//		if (coll.gameObject.tag == "Player")
//			coll.gameObject.GetComponent<Player> ().DealsDamage (10);
//	}

	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere (transform.position, playerRange);
		Gizmos.DrawWireSphere (transform.position, fireRange);
	}
}
