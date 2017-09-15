using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Edificacion:MonoBehaviour {
	[Header("Atributos de tipo edificacion")]
	public Sprite imagen;
	public construccion construccion;

	public abstract bool canConstruct ();
}

[System.Serializable]
public class construccion{
	public string nombre;
	public Recurso [] costoEnRecursos;
	public int vitalidad;
	public Propiedad propiedad;
	public Evolucion evolucion;

	public enum Propiedad
	{
		Defensiva,Constructiva
	}

	public enum Evolucion{
		Bronce,Plata
	}
}


