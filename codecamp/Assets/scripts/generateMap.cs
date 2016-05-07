using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class generateMap : MonoBehaviour {
	public static int row = 10;
	public static int column = 10;
	public static string[,] test = new string[row, column];
	public short nbOfMap;
	public bool isBossSet;
	public static short posX;
	public static short posY;
	public static short portalTaken;
	public static bool[] hasUpgrade = new bool[4];
	public static string[,] healthTaken = new string[10, 2];
	public int hpParse;
	public static string[,] ammoTaken = new string[10, 2];
	public int ammoParse;
	public static bool[] bossBeaten = {false, false, false, false};

	//On va imaginer que les maps se nomment "map_zoneName_nb, et que la zero c'est le boss"
	//1 = special upgrade
	//2 = ammo tank
	// 3 = hp tank
	// Use this for initialization
	void Start () {
		hpParse = 0;
		isBossSet = false;
		Player.maxLife = 200;
		Player.life = Player.maxLife;
		Player.maxAmmo = 30;
		Player.ammo = Player.maxAmmo;
		Player.maxJumps = 1;
		Player.canRun = false;
		Player.canReverse = false;
		for (int i = 0; i < 3; i++)
			hasUpgrade [i] = false;

		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				if (i < 5 && j < 5) {
					test [i, j] = "map_prehistoire_" + Random.Range (4, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
						test [i, j] = "map_prehistoire_" + Random.Range (4, (nbOfMap + 1));
				}
				if (i < 5 && j >= 5) {
					test [i, j] = "map_medieval_" + Random.Range (4, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
						test [i, j] = "map_medieval_" + Random.Range (4, (nbOfMap + 1));
				}
				if (i >= 5 && j < 5) {
					test [i, j] = "map_futur_" + Random.Range (4, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
						test [i, j] = "map_futur_" + Random.Range (4, (nbOfMap + 1));
				}
				if (i >= 5 && j >= 5) {
					test [i, j] = "map_moderne_" + Random.Range (4, (nbOfMap + 1));
					while (test [i, j] == (i == 0 ? "" : test [i - 1, j]) || test [i, j] == (j == 0 ? "" : test [i, j - 1]))
						test [i, j] = "map_moderne_" + Random.Range (4, (nbOfMap + 1));
				}
			}
		}
		while (isBossSet == false){
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
				isBossSet = true;
			}
		}

		int boss2x = Random.Range (5, 10);
		int boss2y = Random.Range (0, 5);
		test [boss2x, boss2y] = "map_futur_0";


		int boss3x = Random.Range (5, 10);
		int boss3y = Random.Range (5, 10);
		test [boss3x, boss3y] = "map_moderne_0";

		int boss4x = Random.Range (0, 5);
		int boss4y = Random.Range (5, 10);
		test [boss4x, boss4y] = "map_medieval_0";

		placeUpgrades (new Vector2(0, 5), new Vector2(0, 5), 2, "map_prehistoire_");
		placeUpgrades (new Vector2(5, 10), new Vector2(0, 5), 3, "map_futur_");
		placeUpgrades (new Vector2(5, 10), new Vector2(5, 10), 3, "map_moderne_");
		placeUpgrades (new Vector2(0, 5), new Vector2(5, 10), 2, "map_medieval_");

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


	void placeUpgrades(Vector2 coor1, Vector2 coor2, short nbMax, string mapName){
		short nbAmmo = 0;
		short nbHp = 0;
		bool upgradePlaced = false;
		while (!upgradePlaced) {
			int upgradex = Random.Range((int)coor1.x, (int)coor1.y);
			int upgradey = Random.Range((int)coor2.x, (int)coor2.y);
			if (test [upgradex, upgradey] != mapName + "0" && test [upgradex, upgradey] != mapName + "1" && test [upgradex, upgradey] != mapName + "2" && test [upgradex, upgradey] != mapName + "3") {
				test [upgradex, upgradey] = mapName + "1";
				upgradePlaced = true;
			}
		}

		while (nbHp != nbMax) {
			int hpX = Random.Range((int)coor1.x, (int)coor1.y);
			int hpY = Random.Range((int)coor2.x, (int)coor2.y);
			if (test [hpX, hpY] != mapName + "0" && test [hpX, hpY] != mapName + "1" && test [hpX, hpY] != mapName + "2" && test [hpX, hpY] != mapName + "3") {
				test [hpX, hpY] = mapName + "3";
				nbHp++;
				string pos = hpX + "" + hpY;
				healthTaken [hpParse, 0] = pos;
				healthTaken [hpParse, 1] = "ko";
				hpParse++;
			}
		}

		while (nbAmmo != nbMax) {
			int ammoX = Random.Range((int)coor1.x, (int)coor1.y);
			int ammoY = Random.Range((int)coor2.x, (int)coor2.y);
			if (test [ammoX, ammoY] != mapName + "0" && test [ammoX, ammoY] != mapName + "1" && test [ammoX, ammoY] != mapName + "2" && test [ammoX, ammoY] != mapName + "3") {
				test [ammoX, ammoY] = mapName + "2";
				nbAmmo++;
				string pos = ammoX + "" + ammoY;
				ammoTaken [ammoParse, 0] = pos;
				ammoTaken [ammoParse, 1] = "ko";
				ammoParse++;
			}
		}

		for (int i = 0; i < hpParse; i++) {
			for (int j = 0; j < 2; j++) {
				Debug.Log(healthTaken [i, j]);
			}
		}
	}




	// Update is called once per frame


	void Update () {
		if (Input.GetKey(KeyCode.Return))
			SceneManager.LoadScene (test[posX, posY]);
	}
}

