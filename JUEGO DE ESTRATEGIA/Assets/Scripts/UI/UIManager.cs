using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// User interface manager.Maneja todo lo que tenga que ver con la interfaz de usuario en la aplicacion.
/// </summary>
public class UIManager : MonoBehaviour {

	public GameObject panelEdificaciones;
	public GameObject panelUnidades;
	public GameObject panelUtilidades;
	public GameObject panelToolkit;
	public static UIManager UImanager;

	void Awake () {
		UImanager = this;
	}

	public void cerrarPanelEdificaciones(){
		if (panelEdificaciones.activeInHierarchy && BuildManager.bM.nombreDelEdificio!="") {
			panelEdificaciones.SetActive (false);
		}
	}

	public void activarPanelToolkit(){
		if (!panelToolkit.activeInHierarchy) {
			panelToolkit.SetActive (true);
			ToolkitInformation.toolkit.mostrarDatos ();
			StartCoroutine (disablePanel(panelToolkit));
		}
	}


	public void setActivePanelToolkit(bool active){
		panelToolkit.SetActive (active);
		StopCoroutine ("disablePanel");
	}

	IEnumerator disablePanel(GameObject panel){
		yield return new WaitForSeconds (20f);
		panel.SetActive (false);
	}

}
