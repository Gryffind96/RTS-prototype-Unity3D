using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Unidad : MonoBehaviour {

	public Sprite foto;
	public string nombre;
	public int vitalidad = 150;
	public NavMeshAgent agent{ get ; private set;}
	Animator anim;

	//variables from animator
	float speed;
	public bool isMoving;
	void Start(){
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	[System.Serializable]
	public enum Clase
	{
		Soldado,Arquero,Ballestero
	}
	public Clase tipo;
	public GameObject HALO;

	public void handleHalo(bool activar){
		HALO.SetActive (activar);
	}

	void Update(){
		isMoving = agent.velocity.magnitude > 0.1f;
	}


	void FixedUpdate(){
		//Debug.Log (agent.remainingDistance);
		anim.SetFloat ("speed", agent.velocity.magnitude);
	}
	public void moveToDestination(Vector3 target){
		agent.isStopped = false;
		agent.radius = 1f;
		agent.Resume ();
		agent.SetDestination (target);
	}

	public void StopMovement(){
		agent.isStopped = true;
	}

	public void attack(GameObject target){
			Debug.Log("disparando evento atacar");
			agent.radius = 1f;
			agent.Resume ();
			agent.SetDestination (target.transform.position);
	}

}
