using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Build manager. Controla todo lo que tiene que ver con construir edificaciones, moverlas, destruirlas etc...
/// </summary>
public class BuildManager : MonoBehaviour,IUpdateInformation<Edificacion>{

	public static BuildManager bM;
	public LayerMask mascaraDeConstruccion;

	public Dictionary<string,GameObject> edificios{ get; private set;}//diccionario de precarga de edificacionesgameobject
	public Edificacion Edificio{ get; private set;}// este es el que se va a instanciar cuando se invoque la subrutina crearEdificacion y se desplazara con el mouse
	public string nombreDelEdificio{ get; private set;}
	private RaycastHit hit;
	private bool estaConstruyendo = false;

	// Use this for initialization
	/// <summary>
	/// Obtenemos las edificaciones prefab de la carpeta resources y lo almacenamos en un dicionario para usarlas cuando se vallan a usar
	/// </summary>
	void Start () {
		bM = this;
		edificios = new Dictionary<string, GameObject> ();

		object[] edif = Resources.LoadAll ("Edificaciones");

		foreach (GameObject item in edif) {
			edificios.Add (item.name, item);
		}

		nombreDelEdificio = string.Empty;
		estaConstruyendo = false;
	}


	// Update is called once per frame
	void LateUpdate () {
		if (nombreDelEdificio!="") {
			estaConstruyendo = true;
			if (Input.GetAxis("Mouse X")!=0 || Input.GetAxis("Mouse Y")!=0) {
				//generamos un rayo hacia el piso con la posicion del mouse  para poner el edificio
				Ray rayo=Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(rayo,out hit,Mathf.Infinity,mascaraDeConstruccion)) {//Si esto pasa le asignamos al edificio la posicion
					if (Edificio!=null) {
						Edificio.transform.position=hit.point;
					} 
				} else {// esto pasa cuando se sale del mapa 
					
				}

			}

			if (Edificio==null) {// para evitar error cuando se cancela y se va a instanciar otra construccion
				return;
			}

			if (Input.GetKeyDown(KeyCode.R)) {// si presiona esta tecla el edificio que esta ubicando lo puede rotar 90 grados 
				Edificio.transform.Rotate (new Vector3 (0, 90, 0));
			}
			if (Input.GetMouseButtonDown(0) && !Edificio.transform.GetComponentInChildren<Colisionador> ().colision) {// terminamos de colocar la edificacion en una posicion fija
				if (Edificio.GetComponent<Edificacion>().canConstruct()) {
					Edificio.transform.GetComponentInChildren<Colisionador> ().setDefaultMaterial();
					Edificio.transform.GetComponentInChildren<Colisionador> ().enabled = false;
					Edificio.GetComponent<Edificacion> ().enabled = true;//aqui activamos el componente externo el edificio ya sea base, generador, cuartel etc
					actualizarInformacionUI (Edificio);
					nombreDelEdificio = string.Empty;
				} else {
					Debug.Log ("No me pude construir");
				}

				estaConstruyendo = false;
				UIManager.UImanager.panelEdificaciones.SetActive (true);
			}
			if (Input.GetMouseButtonDown(1)) {//Si hacemos click derecho es porque el usuario cancelo la construccion entonces destruimos el objeto actual y vaciamos el modo de construir hasta nuevo aviso
				Destroy (Edificio.gameObject);
				crearEdificacion (string.Empty);
				nombreDelEdificio = string.Empty;
				estaConstruyendo = false;
				UIManager.UImanager.panelEdificaciones.SetActive (true);
			}
		}
	}

	/// <summary>
	/// Genera la edificacion de acuerdo a la construccion seleccionada en el UI
	/// </summary>
	/// <param name="nombre_edificio">Nombre edificio.</param>
	public void crearEdificacion(string nombre_edificio){
		if (nombre_edificio!="") {
			
			UIManager.UImanager.setActivePanelToolkit (false);
			nombreDelEdificio = nombre_edificio;
			GameObject go = Instantiate (edificios[nombreDelEdificio],new Vector3(),Quaternion.identity) as GameObject;
			Edificio = go.GetComponent<Edificacion> ();
		}

	}

	/// <summary>
	/// Actualiza la informacion en UI Despues de colocar Una edificacion
	/// </summary>
	/// <param name="building">Building.</param>
	public void actualizarInformacionUI(Edificacion building){//Revisar hay error
		Recurso[] recursos = building.construccion.costoEnRecursos;
		for (int i = 0; i < recursos.Length; i++) {
			
			if (recursos[i].nombre=="Monedas") {
				Global.globales.monedas -= recursos [i].valor;
				Global.globales.textoMonedas.text =Global.globales.monedas +"/"+Global.globales.monedasMax;
			} else {
				if (recursos[i].nombre=="Madera") {
					Global.globales.madera -= recursos [i].valor;
					Global.globales.textoMadera.text =Global.globales.madera +"/"+Global.globales.maderaMax;
				} else {
					if (recursos[i].nombre=="Energia") {
						Global.globales.energia -= recursos [i].valor;
						Global.globales.textoEnergia.text =Global.globales.energia +"/"+Global.globales.energiaMax;
					} else {
						if (recursos[i].nombre=="Plata") {
							Global.globales.plata -= recursos [i].valor;
							Global.globales.textoPlata.text =Global.globales.plata +"/"+Global.globales.plataMax;
						} else {
							if (recursos[i].nombre=="Roca") {
								Global.globales.roca -= recursos [i].valor;
								Global.globales.textoRoca.text =Global.globales.roca +"/"+Global.globales.rocaMax;
							}
						}
					}
				}
			}

		}
	}
}
