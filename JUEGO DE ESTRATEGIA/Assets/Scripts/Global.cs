using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This class Handle all the global resources In the Game
/// </summary>
public class Global : MonoBehaviour {

	public static Global globales;
	public int monedas = 20000;
	public int monedasMax=25000;
	public int madera=10000;
	public int maderaMax=15000;
	public int energia = 7500;
	public int energiaMax=9000;
	public int plata = 5000;
	public int plataMax=6800;
	public int roca= 6000;
	public int rocaMax = 12000;

	public Text textoMonedas;
	public Text textoMadera;
	public Text textoEnergia; 
	public Text textoPlata;
	public Text textoRoca;
	private AudioSource audioPartida;

	/// <summary>
	/// Start this instance when match starts.
	/// </summary>
	void Start(){
		globales = this;
		audioPartida = GetComponent<AudioSource> ();
		textoMonedas.text = monedas + "/" + monedasMax;
		textoMadera.text = madera + "/" + maderaMax;
		textoEnergia.text = energia + "/" + energiaMax;
		textoPlata.text = plata + "/" + plataMax;
		textoRoca.text = roca + "/" + rocaMax;
		audioPartida.Play ();
	}

	/// <summary>
	/// Verifica si dispongo de la cantidad necesaria del recurso actual para comprar esta construccion
	/// </summary>
	/// <returns><c>true</c>, if recurso disponible was verificared, <c>false</c> otherwise.</returns>
	/// <param name="valorActual">Valor actual.</param>
	/// <param name="valorObjectivo">Valor objectivo.</param>
	public bool verificarRecursoDisponible(int valorActual, int valorObjectivo){
		if (valorActual-valorObjectivo <0) {
			return true;
		} else {
			return false;
		}
	}
}
