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
			player.transform.position = new Vector3 (placeX[0], placeY[0], player.transform.position.z);
		if (generateMap.portalTaken == 1)
			player.transform.position = new Vector3 (placeX[1], placeY[1], player.transform.position.z);
		if (generateMap.portalTaken == 2)
			player.transform.position = new Vector3 (placeX[2], placeY[2], player.transform.position.z);
		if (generateMap.portalTaken == 3)
			player.transform.position = new Vector3 (placeX[3], placeY[3], player.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
