using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class generateMap : MonoBehaviour {
	public static int row = 10;
	public static int column = 10;
	public static string[,] test = new string[row, column];
	public short nbOfMap;
	public bool[] isBossSet = new bool[4];
	public static short posX;
	public static short posY;
	public static short portalTaken;

	//On va imaginer que les maps se nomment "map_zoneName_nb, et que la zero c'est le boss"
	//1 = special upgrade
	//2 = ammo tank
	// 3 = hp tank
	// Use this for initialization
	void Start () {
		Player.maxLife = 200;
		Player.life = Player.maxLife;
		Player.maxAmmo = 30;
		Player.ammo = Player.maxAmmo;

		nbOfMap = 3;
		for (int i = 0; i < 3; i++)
			isBossSet [i] = false;
		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				if (i < 5 && j < 5) {
					test [i, j] = "map_prehistoire_" + Random.Range (1, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
						test [i, j] = "map_prehistoire_" + Random.Range (1, (nbOfMap + 1));
				}
				if (i < 5 && j >= 5) {
					test [i, j] = "map_medieval_" + Random.Range (1, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
							test [i, j] = "map_medieval_" + Random.Range (1, (nbOfMap + 1));
				}
				if (i >= 5 && j < 5) {
					test [i, j] = "map_futur_" + Random.Range (1, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
							test [i, j] = "map_futur_" + Random.Range (1, (nbOfMap + 1));
				}
				if (i >= 5 && j >= 5) {
					test [i, j] = "map_moderne_" + Random.Range (1, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
							test [i, j] = "map_moderne_" + Random.Range (1, (nbOfMap + 1));
				}
			}
		}
		while (isBossSet[0] == false){
			int boss1x = Random.Range (0, 5);
			int boss1y = Random.Range (0, 5);
			if (boss1y == 2 && (boss1x == 0 || boss1x == 1)) {
				boss1x = Random.Range (0, 5);
				boss1y = Random.Range (0, 5);
			}
			else if ((boss1y == 3 || boss1y == 1) && boss1x == 0) {
				boss1x = Random.Range (0, 5);
				boss1y = Random.Range (0, 5);
			}
			else {
				test[boss1x, boss1y] = "map_prehistoire_0";
				isBossSet[0] = true;
			}
		}

		int boss2x = Random.Range (5, 10);
		int boss2y = Random.Range (0, 5);
		test [boss2x, boss2y] = "map_medieval_0";
		isBossSet [1] = true;

		int boss3x = Random.Range (5, 10);
		int boss3y = Random.Range (5, 10);
		test [boss3x, boss3y] = "map_moderne_0";
		isBossSet [2] = true;

		int boss4x = Random.Range (0, 5);
		int boss4y = Random.Range (5, 10);
		test [boss4x, boss4y] = "map_futur_0";
		isBossSet [3] = true;

		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				Debug.Log("La map " + i + j + "est " + test [i, j]);
			}
		}
//		Debug.Log(nbOfMap);
		posX = 0;
		posY = 2;
		portalTaken = 1;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return))
			SceneManager.LoadScene (test[posX, posY]);
	}
}
