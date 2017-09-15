using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Colisionador.Esta clase se encarga de manipular todos los eventos de colision para construir edificaciones
/// </summary>
public class Colisionador : MonoBehaviour {

	public bool colision{ get; private set; }

	//visuals effects 
	public Material materialVerde;
	public Material materialRojo;
	public Material materialDefault;


	void OnEnable(){
		if (colision==false) {
			this.gameObject.GetComponent<MeshRenderer> ().material = materialVerde;
		}
	}

	public void setDefaultMaterial(){
		this.gameObject.GetComponent<MeshRenderer> ().material = materialDefault;
	}
	/// <summary>
	/// Detecta si colisiona con otra edificacion
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer==LayerMask.NameToLayer("Edificacion") && other.GetComponent<Colisionador>().isActiveAndEnabled==false) {
			colision = true;
			Debug.Log (colision);
			this.gameObject.GetComponent<MeshRenderer> ().material = materialRojo;
			Debug.Log ("Colisione con"+other.gameObject.transform.parent.name);
		}
	}

	/// <summary>
	/// Detecta si no esta colisionando con una edificacion
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit(Collider other){
		if (other.gameObject.layer==LayerMask.NameToLayer("Edificacion") && other.GetComponent<Colisionador>().isActiveAndEnabled==false) {
			Debug.Log ("me sali de la colision me puedo instanciar");
			colision = false;
			Debug.Log (colision);
			this.gameObject.GetComponent<MeshRenderer> ().material = materialVerde;
		}
	}
}
