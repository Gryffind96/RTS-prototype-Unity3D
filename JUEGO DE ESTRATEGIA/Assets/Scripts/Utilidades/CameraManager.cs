using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	[Header("CameraSettings")]
	public float moveSpeed = 2;
	public float smoothRotation = 2;
	public float maxYAngle=90;

	public float zoomMin=20;
	public float zoomMax=120;
	public float zoom_velocity=20f;
	private Vector2 currentRotation;


	void LateUpdate(){

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		Vector3 pos = transform.position;
		pos += ((Vector3.forward*v)+(Vector3.right*h))*moveSpeed*Time.fixedDeltaTime;
		//pos = new Vector3 (transform.position.x, 10.14f, transform.position.z);

		// si tenemos presionado el click derecho y movemos el mouse podemos rotar la camara a la direccion del mouse(inversa)
		if (Input.GetMouseButton(1) && Input.mousePosition.x!=0) {
			
			currentRotation.x += Input.GetAxis("Mouse X");
			currentRotation.y -= Input.GetAxis("Mouse Y");
			currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
			currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
			transform.rotation = Quaternion.Euler(45,-currentRotation.x,0);
		}

		//Manejamos el campo de vision con el axis de la rueda del mouse
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		pos.y -= scroll * zoom_velocity *100f* Time.deltaTime;
		pos.y = Mathf.Clamp (pos.y, zoomMin, zoomMax);
		transform.position = pos;

	}
}
