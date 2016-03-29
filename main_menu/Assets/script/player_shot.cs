using UnityEngine;
using System.Collections;

public class player_shot : MonoBehaviour
{
	public int damage = 1;
	public bool isEnemyShot = false;

	void Start ()
	{
		Destroy(gameObject, 20);
	}
	void Update ()
	{
	
	}
}
