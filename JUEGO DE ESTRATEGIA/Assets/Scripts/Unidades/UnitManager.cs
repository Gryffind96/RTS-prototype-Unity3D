using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
public class UnitManager : MonoBehaviour {

	[HideInInspector]
	public bool isSelecting = false;
	private Vector3 mousePosTarget;
	[HideInInspector]
	public List<Unidad> unidadesSeleccionadas = new List<Unidad>();
	public LayerMask mascaraDeMovimiento;
	[HideInInspector]
	public static UnitManager unitManager;

	void Start(){
		unitManager = this;
	}

	// Update is called once per frame
	void Update () {
		//empieza a seleccionar el punto inicial para pintar y limpiamos la lista de unidades seleccionadas anteriormente
		if (Input.GetMouseButtonDown (0)) {
			//disable UICosntruction
			isSelecting = true;
			mousePosTarget = Input.mousePosition;
			unidadesSeleccionadas.Clear ();
		}

		// si suelta el click izquierdo despues de pintar el box revisamos si hay unidades en en cuadro si es asi las agregamos a la lista de unidades;
		if (Input.GetMouseButtonUp(0)) {

			foreach (var unidad in FindObjectsOfType<Unidad>()) {
				if (IsWithinSelectionBounds(unidad.gameObject)) {
					unidad.handleHalo (true);
					unidadesSeleccionadas.Add (unidad);
				}

			}
			isSelecting = false;
			bool unitSelected = (unidadesSeleccionadas.Count > 0);
			UIManager.UImanager.panelEdificaciones.SetActive(!unitSelected);
			UIManager.UImanager.panelUnidades.SetActive (unitSelected);
			setUnitsOnPanelUnidades ();

			Debug.Log (unidadesSeleccionadas.Count);
		}

		//maneja el halo de todas las unidades de acuerdo al seleccion box que se esta pintando
		if (isSelecting) {
			foreach (var unidadSeleccionable in FindObjectsOfType<Unidad>()) {
				//si esta dentro de los margenes se activa el halo
				bool esta_En_Recuadro=IsWithinSelectionBounds(unidadSeleccionable.gameObject);
				unidadSeleccionable.handleHalo (esta_En_Recuadro);
			}
		}
		// cuando presionamos click derecho y tenemos unidades seleccionadas las movemos todas
		if (Input.GetButtonDown("Fire2")) {
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100,mascaraDeMovimiento)) {
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Enemies")) {
					Debug.Log ("estoy moviendome en la capa enemies");
					for(int i = 0; i < unidadesSeleccionadas.Count; i++) {
						unidadesSeleccionadas [i].attack (hit.transform.gameObject);//se mueven hacia el target
						StartCoroutine (checkMomevent ());
					}
				} else {
					Debug.Log ("estoy moviendome en la capa terrain");
					for(int i = 0; i < unidadesSeleccionadas.Count; i++) {
						unidadesSeleccionadas [i].moveToDestination (hit.point);//se mueven hacia el target
						StartCoroutine (checkMomevent ());
					}
				}
					

			}


		}
	}

	/// <summary>
	/// Checks the momevent.of the units
	/// </summary>
	/// <returns>The momevent.</returns>
	IEnumerator checkMomevent(){

		yield return new WaitForSeconds (0.5f);
		bool done = false;
		do {

			for (int i = 0; i < unidadesSeleccionadas.Count; i++) {
				if(!unidadesSeleccionadas[i].isMoving){

					for (int j = 0; j < unidadesSeleccionadas.Count; j++) {
						if(j!=i){
							
								unidadesSeleccionadas[j].StopMovement();
						}
					}
					done=true;
				}
			}
			yield return 1;
		} while (!done);
	}

	/// <summary>
	/// Sets the units on PanelUnidades.
	/// </summary>
	public void setUnitsOnPanelUnidades(){

		string typeUnit = "";
		for (int i = 0; i < unidadesSeleccionadas.Count; i++) {


			GameObject referenceOfPanelUnidades = GameObject.Find ("unidad");
			if (i==0) {
				referenceOfPanelUnidades.GetComponent<Image> ().sprite=unidadesSeleccionadas[0].foto;

			} else {
				referenceOfPanelUnidades.transform.Find ("TextCounter").GetComponent<Text>().text=(i+1)+"";
			}


			/*if (i==16) {
				GameObject copy = Instantiate (referenceOfPanelUnidades);
				copy.transform.SetParent (referenceOfPanelUnidades.transform.parent, false);
			}*/
		}
	}
	/// <summary>
	/// Determines whether this instance is within selection bounds the specified gameObject.
	/// </summary>
	/// <returns><c>true</c> if this instance is within selection bounds the specified gameObject; otherwise, <c>false</c>.</returns>
	/// <param name="gameObject">Game object.</param>
	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		if( !isSelecting )
			return false;

		var camera = Camera.main;
		var viewportBounds = Utils.GetViewportBounds( camera, mousePosTarget, Input.mousePosition );
		return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
	}

	/// <summary>
	/// Raises the GU event.Dibuja el cuadro de seleccion de acuerdo a las referencias
	/// </summary>
	void OnGUI()
	{
		if( isSelecting)
		{
			
			// Create a rect from both mouse positions
			var rect = Utils.GetScreenRect( mousePosTarget, Input.mousePosition );
			Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
		}
	}
}
