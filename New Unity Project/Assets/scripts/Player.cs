using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	private Animator	animate;

	public short		jumps;
	public short		maxJumps;
	public bool			ground;
	public short		view;
	public float		life;
	public float		maxLife;
	public float		ammo;
	public float		maxAmmo;
	public Transform	shoot;
	public float		invincibleTime;
	public float		maxInvincibleTime;
	public float		shootDelay;
	public float		maxShootDelay;
	public float		speed;
	public float		attackTime;
	public float		maxAttackTime;
	public float		dmg;
	public float		jumpPower;
	public bool			inPause;
	float				stunTime;
	float				gameSpeed;

	// Use this for initialization
	void Start () {
		animate				= GetComponent<Animator>();

		jumps				= maxJumps;
		life				= maxLife;
		ammo				= maxAmmo;
		invincibleTime		= 0;
		shootDelay			= 0;
		attackTime			= 0;
		view				= 1;
		stunTime			= 0;
		gameSpeed			= Time.timeScale;

		Quaternion tmp = GetComponentInChildren<Weapon> ().transform.rotation;
		tmp.z = Mathf.Deg2Rad * -45f;
		GetComponentInChildren<Weapon> ().transform.rotation = tmp;
	}

	// Update is called once per frame
	void Update () {
		if (stunTime <= 0) {
			if (Input.GetKeyDown (KeyCode.Space) && jumps > 0)
				Jump ();

			if (Input.GetKeyUp (KeyCode.Space) && GetComponent<Rigidbody2D> ().velocity.y > 5)
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -20 * jumpPower));
			
			if (Input.GetKey (KeyCode.LeftArrow)) {
				GetComponent<Rigidbody2D> ().position += new Vector2 (-1 * speed * Time.deltaTime, 0);
				view = -1;
				Vector3 scale = transform.localScale;
				scale.x *= scale.x < 0 ? 1 : -1;
				transform.localScale = scale;
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				GetComponent<Rigidbody2D> ().position += new Vector2 (speed * Time.deltaTime, 0);
				view = 1;
				Vector3 scale = transform.localScale;
				scale.x *= scale.x > 0 ? 1 : -1;
				transform.localScale = scale;
			}

			if (Input.GetKey (KeyCode.A))
				summonAmmo ();

			if (Input.GetKey (KeyCode.S) && attackTime <= 0) {
				attackTime = maxAttackTime;
				GetComponentInChildren<Weapon> ().attack = true;
				Quaternion tmp = GetComponentInChildren<Weapon> ().transform.rotation;
				tmp.z = Mathf.Deg2Rad * -45f;
				GetComponentInChildren<Weapon> ().transform.rotation = tmp;
			}
			animatePlayer ();
		} else
			animate.Play ("samus");

		if (life <= 0) {
			life = 0;
			Destroy (this.gameObject);
		}

//		if (Input.GetKeyDown (KeyCode.Return))
//			pause ();

		if (invincibleTime > 0)
			invincibleTime -= Time.deltaTime;
		if (stunTime > 0)
			stunTime -= Time.deltaTime;
		if (shootDelay > 0)
			shootDelay -= Time.deltaTime;
		if (attackTime > 0) {
			attackTime -= Time.deltaTime;
			GetComponentInChildren<Weapon> ().GetComponent<SpriteRenderer>().enabled = true;
			GetComponentInChildren<Weapon> ().transform.Rotate (new Vector3(0, 0, -10f));
		} else {
			GetComponentInChildren<Weapon> ().attack = false;
			GetComponentInChildren<Weapon> ().GetComponent<SpriteRenderer>().enabled = false;
		}
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (invincibleTime % 0.1 < 0.05 && invincibleTime > 0 ? 0 : 1));
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Jump") {
			maxJumps++;
			jumps++;
			Destroy (coll.gameObject);
		}
		if (coll.gameObject.tag == "Regen") {
			life = maxLife;
			Destroy(coll.gameObject);
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Platform") {
			float angle = Mathf.Cos (30.0f * Mathf.Deg2Rad);
			foreach (ContactPoint2D contact in coll.contacts) {
				ground = Vector3.Dot (contact.normal, Vector3.up) > angle;
				if (ground)
					jumps = maxJumps;
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Platform")
			ground = false;
	}

	public void Jump() {
		if (ground)
			jumps = maxJumps;
		GetComponent<Rigidbody2D> ().velocity = Vector2.up * jumpPower;
		jumps--;
	}

	public void DealsDamage(float damage, bool left = true) {
		if (invincibleTime <= 0) {
			life -= damage;
			invincibleTime = maxInvincibleTime;
			if (left)
				transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-5, 3);
			else
				transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (5, 3);
		}
	}

	void summonAmmo() {
		if (shootDelay <= 0 && ammo > 0) {
			Vector3 shPos = transform.position;
			if (Input.GetKey (KeyCode.UpArrow))
				shPos.x += transform.localScale.x / 20;
			if (Input.GetKey (KeyCode.DownArrow))
				shPos.y -= transform.localScale.y / 16;
			else
				shPos.y += transform.localScale.y / 26;
			Instantiate (shoot, shPos, transform.rotation);
			ammo--;
			shootDelay = maxShootDelay;
		}
	}

	void animatePlayer() {
		if (!ground && GetComponent<Rigidbody2D> ().velocity.y != 0)
			animate.Play ("samus_jump");
		else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow))
			animate.Play ("samus_run");
		else if (Input.GetKey (KeyCode.UpArrow))
			animate.Play ("samus_up");
		else if (Input.GetKey (KeyCode.DownArrow))
			animate.Play ("samus_down");
		else if (Input.GetKey (KeyCode.A))
			animate.Play ("samus_shoot");
		else
			animate.Play("samus");
	}

//	void pause() {
//		if (!inPause) {
//			Time.timeScale = 0f;
//			inPause = true;
//		} else {
//			Time.timeScale = gameSpeed;
//			inPause = false;
//		}
//	}

	public void Stun(float time) {
		if (ground)
			stunTime = time;
	}
}
