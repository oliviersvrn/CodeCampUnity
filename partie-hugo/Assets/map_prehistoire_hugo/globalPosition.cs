using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class globalPosition : MonoBehaviour {

	public short idPortal;
	public BoxCollider2D box;

	// Use this for initialization
	void Start () {
//		for (int i = 0; i < generateMap.row; i++) {
//			for (int j = 0; j < generateMap.column; j++) {
//				Debug.Log("La map " + i + j + "est " + generateMap.test [i, j]);
//			}
//		}

		if (generateMap.posX == 0 && idPortal == 0) {
			box.enabled = false;
		}
		if (generateMap.posX == 9 && idPortal == 1) {
			box.enabled = false;
		}
		if (generateMap.posY == 0 && idPortal == 3) {
			box.enabled = false;
		}
		if (generateMap.posY == 9 && idPortal == 2) {
			box.enabled = false;
		}
//		Debug.Log(generateMap.posX);
//		Debug.Log (generateMap.posY);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D coll){
//		Debug.Log ("aha");
		if (coll.gameObject.tag == "Player") {
			generateMap.portalTaken = idPortal;	
			if (idPortal == 0) {
				generateMap.posX -= 1;
				SceneManager.LoadScene (generateMap.test [generateMap.posX, generateMap.posY]);
			}
			if (idPortal == 1) {
				generateMap.posX += 1;
				SceneManager.LoadScene (generateMap.test [generateMap.posX, generateMap.posY]);
			}
			if (idPortal == 2) {
				generateMap.posY += 1;
				SceneManager.LoadScene (generateMap.test [generateMap.posX, generateMap.posY]);
			}
			if (idPortal == 3) {
				generateMap.posY -= 1;
				SceneManager.LoadScene (generateMap.test [generateMap.posX, generateMap.posY]);
			}
		}
	}
}
