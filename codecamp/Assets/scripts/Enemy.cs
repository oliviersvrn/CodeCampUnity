using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	public int				dmg;
	public float			maxLife;
	float					life;
	float					invincibleTime;
	public float			maxInvincibleTime;
	public float			speed;
	public GameObject		player;
	public float			viewRange;
	public bool				ground;
	public bool				canBePush;
	public bool				canDropHealth;
	public bool				canDropAmmo;
	public GameObject		ammo;
	public GameObject		health;
	// Use this for initialization
	public void Init () {
		life				= maxLife;
		maxInvincibleTime	= 0.15f;
		invincibleTime		= 0f;
		player				= GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	public void Up () {
		if (life <= 0)
			Destroy (transform.gameObject);
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1.5f - invincibleTime / maxInvincibleTime, 1.5f - invincibleTime / maxInvincibleTime, 1);
		if (invincibleTime > 0)
			invincibleTime -= Time.deltaTime;
	}

	public void DealsDamage(float dmg, float knockBack = 1) {
		if (invincibleTime <= 0) {
			life -= dmg;
			invincibleTime = maxInvincibleTime;
			if (player.transform.position.x > transform.position.x && canBePush)
				transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-1 * knockBack, 3);
			else if (canBePush)
				transform.GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockBack, 3);
		}
	}

	public static float Distance(Vector2 pos1, Vector2 pos2) {
		Vector2 tmp;
		tmp = pos1 - pos2;
		return (Mathf.Sqrt (tmp.x * tmp.x + tmp.y * tmp.y));
	}

	public void Attack(GameObject obj, string animName = "") {
		Animator anim = GetComponent<Animator> ();
		obj.GetComponent<Player> ().DealsDamage(dmg, transform.position.x > player.transform.position.x);
		anim.Play (animName + "_attack");
	}

	public void Move(string animName, bool reverse = false) {
		Animator anim = GetComponent<Animator> ();
		float time = (anim.GetCurrentAnimatorStateInfo (0).IsName (animName + "_turn")) ? 
			anim.GetCurrentAnimatorStateInfo (0).normalizedTime % 1 : 1;

		if (Distance (transform.position, player.transform.position) < viewRange) {
			Vector3 scale = transform.localScale;

			if (!reverse && transform.position.x < player.transform.position.x ||
				reverse && transform.position.x > player.transform.position.x) {
				GetComponent<Rigidbody2D> ().position += new Vector2 (speed * time * Time.deltaTime, 0);

				if (scale.x < 0) {
					anim.Play (animName + "_turn");
					if (time != 1 && time >= 0.7)
						scale.x *= -1;
				}
			} else {
				GetComponent<Rigidbody2D> ().position -= new Vector2 (speed * time * Time.deltaTime, 0);

				if (scale.x > 0) {
					anim.Play (animName + "_turn");
					if (time != 1 && time >= 0.7)
						scale.x *= -1;
				}
			}
			transform.localScale = scale;
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName (animName + "_attack") &&
			    !anim.GetCurrentAnimatorStateInfo (0).IsName (animName + "_turn"))
				anim.Play (animName + "_run");
		} else {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName (animName + "_attack") &&
				!anim.GetCurrentAnimatorStateInfo (0).IsName (animName + "_turn"))
				anim.Play (animName);
		}
	}

	public void CollisionEnter2D(Collision2D coll) {
	}

	public void CollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Platform") {
			float GroundAngleTolerance = Mathf.Cos (30.0f * Mathf.Deg2Rad);
			foreach (ContactPoint2D contact in coll.contacts) {
				if (Vector3.Dot (contact.normal, Vector3.up) > GroundAngleTolerance)
					ground = true;
			}
		}
		if (coll.gameObject.tag == "Player") {
			player.GetComponent<Player>().DealsDamage(dmg, transform.position.x > coll.gameObject.transform.position.x);
		}
	}

	public void CollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Platform")
			ground = false;
	}

	void OnDestroy() {
		int rand = Random.Range (0, 5);
		if (rand < 2) {
			if (canDropAmmo && !canDropHealth)
				Instantiate (ammo, transform.position, transform.rotation);
			else if (!canDropAmmo && canDropHealth)
				Instantiate (health, transform.position, transform.rotation);
			else if (canDropAmmo && canDropHealth)
				Instantiate ((rand == 0 ? health : ammo), transform.position, transform.rotation);
		}
	}

	public void IsBoss() {
		Camera.main.gameObject.GetComponent<LifeBar> ().boss = true;
		Camera.main.gameObject.GetComponent<LifeBar> ().bossLife = life;
		Camera.main.gameObject.GetComponent<LifeBar> ().maxBossLife = maxLife;
	}
}
