using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	int i;
	float shakeTime;
	float maxShakeTime;
	public Vector3 InitPos;
	public float shakePower;
	// Use this for initialization
	void Start () {
		shakeTime = 0f;
		InitPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		Vector3 cameraPos = transform.position;
		cameraPos = InitPos;
		if (shakeTime > 0) {
			shakeTime -= Time.deltaTime;
			cameraPos.x += Random.Range (-1 * shakePower * shakeTime / maxShakeTime, shakePower * shakeTime / maxShakeTime);
			cameraPos.y += Random.Range (-1 * shakePower * shakeTime / maxShakeTime, shakePower * shakeTime / maxShakeTime);
		}
		transform.position = cameraPos;
	}

	public void Shake(float time) {
		shakeTime = time;
		maxShakeTime = time;
	}
}
