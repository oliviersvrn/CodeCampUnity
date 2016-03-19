using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float dmg;
	public bool attack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float RealDamage() {
		return (attack ? dmg : 0);
	}
}
