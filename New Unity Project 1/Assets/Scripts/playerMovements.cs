using UnityEngine;
using System.Collections;

public class playerMovements : MonoBehaviour {
    private CharacterController control; // recuperer les controles du joueur
    private Animator animate; // animer le sprite
	public string gameover;
    public short jumps;
    public short ttlJumps;
    public bool onGround;
	public AudioClip[] audioclip;
	float playerHeightY;
	private Transform ground;
	private Transform player;
	public short view;
    public float        life;
    public float        maxLife;
    public Transform    shoot;
    public float        invincibleTime;
    public float        invincibleTimeMax;
    public float        shootDelay;
    public float        shootDelayMax;
    void Start ()
    {
        animate = GetComponent<Animator>();
        control = GetComponent<CharacterController>();
        ttlJumps = 1;
        jumps = ttlJumps;
		view = 1;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		ground = GameObject.FindGameObjectWithTag ("Ground").transform;
        maxLife                = 1000;
        life                = maxLife;
        invincibleTimeMax    = 1f;
        invincibleTime        = invincibleTime;
        shootDelayMax        = 0.2f;
        shootDelay            = shootDelay;

    }

    void Update ()
    {
		playerHeightY = player.position.y;
        if (Input.GetKey (KeyCode.RightArrow))
        {
			if (Input.GetKey(KeyCode.LeftShift))
				transform.position -= new Vector3(-20 * Time.deltaTime, 0, 0);
			else
				transform.position -= new Vector3(-8 * Time.deltaTime, 0, 0);
			transform.localScale = new Vector3(8, 8, 8);
			view = 1;
        }
        else if (Input.GetKey (KeyCode.LeftArrow))
        {
			if (Input.GetKey(KeyCode.LeftShift))
				transform.position -= new Vector3(20 * Time.deltaTime, 0, 0);
			else
	            transform.position -= new Vector3(8 * Time.deltaTime, 0, 0);
			transform.localScale = new Vector3(-8, 8, 8);
			view = -1;
        }
        if (!onGround && GetComponent<Rigidbody2D>().velocity.y != 0)
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
         if (Input.GetKeyDown (KeyCode.Space) && jumps > 0) {
            GetComponent<Rigidbody2D> ().velocity = Vector2.up * 20;
            jumps--;
			AudioSource audio = GetComponent<AudioSource> ();
			audio.PlayOneShot (audioclip [0]);
        }

        if (Input.GetKeyUp (KeyCode.Space) && GetComponent<Rigidbody2D> ().velocity.y > 5)
            GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -700));
		if (playerHeightY < -10)
			DieByFall ();

        if (life <= 0)
            Destroy (this.gameObject);

        if (Input.GetKey (KeyCode.A) && shootDelay <= 0) {
            Vector3 shPos = transform.position;
            if (Input.GetKey (KeyCode.UpArrow))
                shPos.x += transform.localScale.x / 20;
            if (Input.GetKey (KeyCode.DownArrow))
                shPos.y -= transform.localScale.y / 16;
            else
                shPos.y += transform.localScale.y / 26;
            Instantiate (shoot, shPos, transform.rotation);
            shootDelay = shootDelayMax;
        }

        if (invincibleTime > 0)
            invincibleTime -= Time.deltaTime;
        if (shootDelay > 0)
            shootDelay -= Time.deltaTime;
		if (life == 0)
			DieByFall ();
    }

    

	void DieByFall()
	{
		Application.LoadLevel (gameover);
	}
	void PlaySound(int clip)
	{
		AudioSource audio = GetComponent<AudioSource> ();
		audio.PlayOneShot (audioclip [clip]);
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Jump") {
            ttlJumps++;
            jumps++;
            Destroy (coll.gameObject);
        }
		if (coll.gameObject.tag == "Ground") {
			jumps = ttlJumps;
			onGround = true;
		}
        }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
            onGround = false;
    }

    public void DealDamage(float damage) {
		if (invincibleTime <= 0) {
			life -= damage;
			invincibleTime = invincibleTimeMax;
		}
    }
}