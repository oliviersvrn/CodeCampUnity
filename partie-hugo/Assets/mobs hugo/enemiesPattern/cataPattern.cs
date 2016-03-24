using UnityEngine;
using System.Collections;

public class cataPattern : MonoBehaviour {
	public float playerRange;
	public bool playerInRange;
	public LayerMask playerLayer;
	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		moveTruc ();
	}

	void moveTruc(){
		playerInRange = Physics2D.OverlapCircle (transform.position, playerRange, playerLayer);
		if (player.position.y > transform.position.y)
			player.position -= new Vector3(0, -20 * Time.deltaTime, 0);
	}
	void OnDrawGizmosSelected(){
		Gizmos.DrawWireSphere (transform.position, playerRange);
	}
		
}
