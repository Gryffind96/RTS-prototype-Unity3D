using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Toolkit information.Clase que manipula la informacion de la construccion que se va a poner en el mapa de juego a nivel de interfaz de usuario
/// </summary>
public class ToolkitInformation : MonoBehaviour{

	public static ToolkitInformation toolkit;
	public Text centalText;
	public GameObject textoInformativo;
	private List<GameObject> textos= new List<GameObject>();

	public string edificioToPlace ;
	void Start(){
		
		toolkit = this;
		UIManager.UImanager.setActivePanelToolkit (false);
	}
	public void mostrarDatos(){//mientras que este activa la herramienta mostraremos la informacion de la construccion actual

		textos = new List<GameObject> ();
		centalText.text =edificioToPlace;

		foreach (var item in BuildManager.bM.edificios[centalText.text].GetComponent<Edificacion>().construccion.costoEnRecursos) {
			GameObject goText = Instantiate (textoInformativo);
			goText.transform.SetParent (this.transform, false);
			goText.GetComponent<Text>().text = item.nombre + " :" + item.valor + "\n";
			textos.Add (goText);
		}

	}

	void OnDisable(){//limpiamos la informacion para cuando ya no este activo 
		for (int i = 0; i < textos.Count; i++) {
			Destroy (textos[i]);
		}
	}

}
