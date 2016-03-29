using UnityEngine;
using System.Collections;

public class player_manager : MonoBehaviour {
	private CharacterController control; // recuperer les controles du joueur
	private Animator animate; // animer le sprite

	public short jumps;
	public short ttlJumps;
	public bool ground;

	void Start ()
	{
		animate = GetComponent<Animator>();
		control = GetComponent<CharacterController>();
		ttlJumps = 1;
		jumps = ttlJumps;
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.RightArrow))
		{
			transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
			transform.localScale = new Vector3(2, 2, 2);
		}
		else if (Input.GetKey (KeyCode.LeftArrow))
		{
			transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
			transform.localScale = new Vector3(-2, 2, 2);
		}
		if (!ground && GetComponent<Rigidbody2D>().velocity.y != 0)
			animate.Play ("samus_jump");
		else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow))
			animate.Play("samus_run");
		else if (Input.GetKey (KeyCode.UpArrow))
				animate.Play("samus_up");
		else if (Input.GetKey (KeyCode.DownArrow))
			animate.Play("samus_down");
		else if (Input.GetKey (KeyCode.A))
			animate.Play("samus_shot");
		else
			animate.Play("samus");
		if (Input.GetKeyDown (KeyCode.Space) && jumps > 0)
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * 4;
			jumps--;
		}
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			ground = true;
			jumps = ttlJumps;
		}
	}
	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
			ground = false;
	}
}