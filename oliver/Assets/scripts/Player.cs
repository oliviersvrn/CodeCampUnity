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
	public bool			inPause;
	float				stunTime;
	float				gameSpeed;

	// Use this for initialization
	void Start () {
		animate				= GetComponent<Animator>();

		jumps				= maxJumps;
		life				= maxLife;
		ammo				= maxAmmo;
		invincibleTime		= maxInvincibleTime;
		shootDelay			= maxShootDelay;
		attackTime			= maxAttackTime;
		view				= 1;
		speed				= 7;
		dmg					= 50;
		stunTime			= 0;
		gameSpeed			= Time.timeScale;
	}

	// Update is called once per frame
	void Update () {
		if (stunTime <= 0) {
			if (Input.GetKeyDown (KeyCode.Space) && jumps > 0) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.up * 20;
				jumps--;
			}

			if (Input.GetKeyUp (KeyCode.Space) && GetComponent<Rigidbody2D> ().velocity.y > 5)
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -500));
			
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
			}
			animatePlayer ();
		} else
			animate.Play ("samus");

		if (life <= 0)
			Destroy (this.gameObject);

		if (Input.GetKeyDown (KeyCode.Return))
			pause ();

		if (invincibleTime > 0)
			invincibleTime -= Time.deltaTime;
		if (stunTime > 0)
			stunTime -= Time.deltaTime;
		if (shootDelay > 0)
			shootDelay -= Time.deltaTime;
		if (attackTime > 0)
			attackTime -= Time.deltaTime;
		else
			GetComponentInChildren<Weapon> ().attack = false;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (invincibleTime % 0.1 < 0.05 && invincibleTime > 0 ? 0 : 1));

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Jump") {
			maxJumps++;
			jumps++;
			Destroy (coll.gameObject);
		}
		if (coll.gameObject.tag == "Ground") {
			ground = true;
			jumps = maxJumps;
		}
		if (coll.gameObject.tag == "Regen") {
			life = maxLife;
			Destroy(coll.gameObject);
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			DealsDamage(coll.gameObject.GetComponent<Enemy> ().dmg, transform.position.x < coll.gameObject.transform.position.x);
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground")
			ground = false;
	}

	public void DealsDamage(float damage, bool left = true) {
		if (invincibleTime <= 0) {
			life -= damage;
			invincibleTime = maxInvincibleTime;
			if (left)
				transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-30, 10);
			else
				transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (30, 10);
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

	void pause() {
		if (!inPause) {
			Time.timeScale = 0f;
			inPause = true;
		} else {
			Time.timeScale = gameSpeed;
			inPause = false;
		}
	}

	public void Stun(float time) {
		if (ground)
			stunTime = time;
	}
}
