using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	int i;
	public float shakeTime;
	public float maxShakeTime;
	// Use this for initialization
	void Start () {
		shakeTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		Vector3 cameraPos = transform.position;
		cameraPos.x = playerPos.x;
		cameraPos.y = playerPos.y;
		if (cameraPos.x > 10)
			cameraPos.x = 10;
		if (cameraPos.x < -10)
			cameraPos.x = -10;
		if (cameraPos.y > 2.5f)
			cameraPos.y = 2.5f;
		if (cameraPos.y < -2)
			cameraPos.y = -2;
		if (shakeTime > 0) {
			shakeTime -= Time.deltaTime;
			cameraPos.x += Random.Range (-0.5f * shakeTime / maxShakeTime, 0.5f * shakeTime / maxShakeTime);
			cameraPos.y += Random.Range (-0.5f * shakeTime / maxShakeTime, 0.5f * shakeTime / maxShakeTime);
		}
		transform.position = cameraPos;
	}

	public void Shake(float time) {
		shakeTime = time;
		maxShakeTime = time;
	}
}
