using System.Collections;
using UnityEngine;

public interface IRecolectable<T>{

	void generarRecurso (T recurso);
}

public interface ISpendable<T>{
	void gastarRecurso (T recurso);
}

public interface IUpdateInformation<T>{
	void actualizarInformacionUI (T Objecto);
}