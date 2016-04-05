using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class globalPosition : MonoBehaviour {

	public short idPortal;
	public BoxCollider2D box;
	public bool canEnter;
	bool edge;

	// Use this for initialization
	void Start () {
//		for (int i = 0; i < generateMap.row; i++) {
//			for (int j = 0; j < generateMap.column; j++) {
//				Debug.Log("La map " + i + j + "est " + generateMap.test [i, j]);
//			}
//		}
		canEnter = true;
		if (generateMap.posX == 0 && idPortal == 0) {
			edge = true;
		}
		if (generateMap.posX == 9 && idPortal == 1) {
			edge = true;
		}
		if (generateMap.posY == 0 && idPortal == 3) {
			edge = true;
		}
		if (generateMap.posY == 9 && idPortal == 2) {
			edge = true;
		}
		Debug.Log (generateMap.posX);
		Debug.Log (generateMap.posY);
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.GetComponent<LifeBar> ().boss)
			canEnter = false;
		else
			canEnter = !edge;
		if (!canEnter)
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("fakePortal");
		else
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("portal_" + (idPortal / 2 + 1));
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player" && canEnter) {
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
