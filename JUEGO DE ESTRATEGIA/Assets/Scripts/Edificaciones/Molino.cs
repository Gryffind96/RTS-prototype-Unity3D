using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molino : Edificacion,IRecolectable<int> {

	[Header("Atributos de tipo molino")]
	public int energia = 10;
	public float waitingTime=0.5f;
	private float currentTime=0f;

	/// <summary>
	/// Generars the recurso.
	/// </summary>
	/// <param name="cantidad">Cantidad.</param>
	public void generarRecurso(int cantidad){
		Global.globales.energia += cantidad;
		Global.globales.textoEnergia.text=Global.globales.energia +"/"+Global.globales.energiaMax;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime>waitingTime) {
			currentTime = 0f;
			generarRecurso (energia);
		}
		Debug.Log (currentTime);
	}

	public void activarFuncionalidad(bool activar){
		this.enabled = activar;
	}

	public override bool canConstruct ()
	{
		Recurso[] recursos = this.construccion.costoEnRecursos;
		if (recursos[0].valor<=Global.globales.monedas && recursos[1].valor<= Global.globales.madera && recursos[2].valor<= Global.globales.energia && recursos[3].valor<= Global.globales.roca) {
			return true;
		}
		return false;
	}
}
