using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {
	public float life;
	public float maxLife;
	public float bossLife;
	public float maxBossLife;
	public float ammo;
	public float maxAmmo;

	public GUIStyle lifeBar;
	public GUIStyle background;
	public GUIStyle ammoBar;
	public GUIStyle ammoBackground;
	public GUIStyle bossBar;
	public GUIStyle bossBackground;
	public bool boss;

	void Start() {
		boss = false;
	}

	void Update() {
		maxLife = Player.maxLife;
		life = Player.life;
		ammo = Player.ammo;
		maxAmmo = Player.maxAmmo;
	}

	void OnGUI () {
		float length = life / maxLife * 106;
		GUI.Box(new Rect(10, 10, 172, 20), "", background);
		GUI.Box(new Rect(72, 16, length, 8), "", lifeBar);

		length = ammo / maxAmmo * 106;
		GUI.Box(new Rect(10, 40, 172, 20), "", ammoBackground);
		GUI.Box(new Rect(72, 46, length, 8), "", ammoBar);

		if (boss) {
			length = bossLife / maxBossLife * 106;
			GUI.Box(new Rect(Screen.width - 182, 10, 172, 20), "", background);
			GUI.Box(new Rect(Screen.width - length - 16, 16, length, 8), "", lifeBar);
		}
	}
}
