using UnityEngine;
using System.Collections;

public class keepMusic : MonoBehaviour {
	
	public AudioSource[] musics;
	public static AudioSource test;
	public static short musicCount;
	public static string actualmusic;
	// Use this for initialization
	void Start () {
		test = GetComponent<AudioSource> ();
		DontDestroyOnLoad (test);
	}

	// Update is called once per frame
	void Update () {
		AudioClip tmp = test.clip;
		if (generateMap.posX < 5 && generateMap.posY < 5) {
			if (Camera.main.GetComponent<LifeBar> ().boss)
				test.clip = musics [1].clip;
			else if (test.clip != musics[0].clip)
				test.clip = musics [0].clip;
		}
		else if (generateMap.posX < 5 && generateMap.posY >= 5) {
			if (Camera.main.GetComponent<LifeBar> ().boss)
				test.clip = musics [3].clip;
			else
				test.clip = musics [2].clip;
		}
		else if (generateMap.posX >= 5 && generateMap.posY >= 5) {
			if (Camera.main.GetComponent<LifeBar> ().boss)
				test.clip = musics [5].clip;
			else
				test.clip = musics [4].clip;
		}
		else if (generateMap.posX >= 5 && generateMap.posY < 5) {
			if (Camera.main.GetComponent<LifeBar> ().boss)
				test.clip = musics [7].clip;
			else
				test.clip = musics [6].clip;
		}
		if (tmp != test.clip)
			test.Play ();
	}
}
