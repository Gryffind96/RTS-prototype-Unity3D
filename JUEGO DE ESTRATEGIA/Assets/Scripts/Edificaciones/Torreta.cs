using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : Edificacion {

	public void activarFuncionalidad(bool activar){
		this.enabled = activar;
	}

	public override bool canConstruct ()
	{
		Recurso[] recursos = this.construccion.costoEnRecursos;
		if (recursos[0].valor<=Global.globales.roca && recursos[1].valor<= Global.globales.monedas) {
			return true;
		}
		return false;
	}
}
