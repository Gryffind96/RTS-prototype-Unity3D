using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Event controller.Manipula todos los eventos con las interfaces de eventSystem como los eventos del mouse sobre los botones
/// </summary>
public class EventController : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler{

	public void OnPointerEnter(PointerEventData data){
		ToolkitInformation.toolkit.edificioToPlace = gameObject.name;
		UIManager.UImanager.activarPanelToolkit ();
	}

	public void OnPointerExit(PointerEventData data){
		UIManager.UImanager.setActivePanelToolkit (false);
	}

	public void OnPointerClick(PointerEventData data){
		if (UnitManager.unitManager.isSelecting && UnitManager.unitManager.unidadesSeleccionadas.Count==0) {
			Debug.Log ("disparado evento o pointer click");
			UIManager.UImanager.cerrarPanelEdificaciones ();
		}
	}

}
