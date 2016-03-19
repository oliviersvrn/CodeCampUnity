using UnityEngine;
using System.Collections;

public class platformsGeneration : MonoBehaviour {
	float playerHeightY;
	private float platCheck;
	float y;
	public Transform platform;
	public Transform player;
	// Use this for initialization
	void Start () {
		y = Random.Range (2f, 10f);
		PlatformSpawner (y);
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		playerHeightY = player.position.y;
		if (playerHeightY > platCheck)
			PlatformManager ();
	}

	void PlatformManager()
	{
		platCheck = player.position.y + 3;
		y = Random.Range ((platCheck - 5), (platCheck + 3));
		PlatformSpawner (y);
	}

	void PlatformSpawner(float y){
		float x = Random.Range (-10f, -6f);

		Instantiate (platform, new Vector3 (x, y, 0), Quaternion.identity);
	}
}
