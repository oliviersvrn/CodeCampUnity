using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {
	public float life;
	public float maxLife;
	public float bossLife;
	public float maxBossLife;

	public GUIStyle lifeBar;
	public GUIStyle background;
	public bool boss;

	void Start() {
		boss = false;
	}

	void Update() {
		maxLife = Player.maxLife;
		life = Player.life;
	}

	void OnGUI () {
		float length = life / maxLife * 400;
		GUI.Box(new Rect(10, 10, 412, 52), "", background);
		GUI.Box(new Rect(16, 16, length, 40), "", lifeBar);

		if (boss) {
			length = bossLife / maxBossLife * 400;
			GUI.Box(new Rect(Screen.width - 432, 10, 412, 52), "", background);
			GUI.Box(new Rect(Screen.width - length - 26, 16, length, 40), "", lifeBar);
		}
	}
}
