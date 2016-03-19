using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int dmg;
	public float maxLife;
	float life;
	float invincibleTime;
	public float maxInvincibleTime;
	public float speed;
	public GameObject player;
	public float viewRange;
	// Use this for initialization
	public void Init () {
		maxLife = 100f;
		life = maxLife;
		maxInvincibleTime = 0.15f;
		invincibleTime = maxInvincibleTime;
	}

	// Update is called once per frame
	public void Up () {
		if (life <= 0)
			Destroy (transform.gameObject);
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1.5f - invincibleTime / maxInvincibleTime, 1.5f - invincibleTime / maxInvincibleTime, 1);
		if (invincibleTime > 0)
			invincibleTime -= Time.deltaTime;
	}

	void OnTriggerStay2D (Collider2D coll) {
		if (coll.gameObject.tag == "Weapon" && coll.gameObject.GetComponent<Weapon>().attack)
			DealsDamage(coll.gameObject.GetComponent<Weapon> ().RealDamage());
	}

	public void DealsDamage(float dmg) {
		if (invincibleTime <= 0) {
			life -= dmg;
			invincibleTime = maxInvincibleTime;
		}
	}

	public float Distance(Vector2 pos1, Vector2 pos2) {
		Vector2 tmp;
		tmp = pos1 - pos2;
		return (Mathf.Sqrt (tmp.x * tmp.x + tmp.y * tmp.y));
	}

	public void Attack(GameObject obj, string animName) {
		Animator anim = GetComponent<Animator> ();
		obj.GetComponent<Player> ().DealsDamage(dmg);
		anim.Play (animName + "_attack");
	}

	public void Move(string animName) {
		Animator anim = GetComponent<Animator> ();
		float time = (anim.GetCurrentAnimatorStateInfo (0).IsName (animName + "_turn")) ? 
			anim.GetCurrentAnimatorStateInfo (0).normalizedTime % 1 : 1;
		if (Distance (transform.position, player.transform.position) < viewRange) {
			Vector3 scale = transform.localScale;
			if (transform.position.x < player.transform.position.x) {
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
}
