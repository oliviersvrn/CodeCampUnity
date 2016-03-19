using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour {

	public int dmg;
    public short view;
	private playerMovements player;
    // Use this for initialization
    void Start () {
		player = FindObjectOfType<playerMovements> ();
        dmg = 5;
		view = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovements>().view;
        if (Input.GetKey(KeyCode.UpArrow))
            GetComponent<Rigidbody2D> ().velocity = (new Vector2 (0, 20));
		else if (Input.GetKey(KeyCode.DownArrow) && !player.onGround)
			GetComponent<Rigidbody2D> ().velocity = (new Vector2 (0, -20));
        else
            GetComponent<Rigidbody2D> ().velocity = (new Vector2 (20 * view, 0));
    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "enemy") {
			coll.gameObject.GetComponent<pteggroPattern> ().hp -= 10;
		
		}
		if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "bullet")
            Destroy (transform.gameObject);
    }

    void OnBecameInvisible() {
        Destroy (transform.gameObject);
    }
}
