using UnityEngine;
using System.Collections;

public class positionPlayer : MonoBehaviour {
	public float[] placeX = new float[4];
	public float[] placeY = new float[4];
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (generateMap.portalTaken == 0)
			player.transform.position = new Vector2 (placeX[0], placeY[0]);
		if (generateMap.portalTaken == 1)
			player.transform.position = new Vector2 (placeX[1], placeY[1]);
		if (generateMap.portalTaken == 2)
			player.transform.position = new Vector2 (placeX[2], placeY[2]);
		if (generateMap.portalTaken == 3)
			player.transform.position = new Vector2 (placeX[3], placeY[3]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
