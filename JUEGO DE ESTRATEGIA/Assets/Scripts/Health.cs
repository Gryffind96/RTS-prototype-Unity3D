using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {


	public float healthMax=100;
	private float currentHealth=0;
	// Use this for initialization
	void Start () {
		currentHealth = healthMax;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(float amount){
		if (currentHealth<=0) {
			Debug.Log ("estoy muerto");
			//execute die 
		} else {
			currentHealth -= amount;
		}
	}
}
