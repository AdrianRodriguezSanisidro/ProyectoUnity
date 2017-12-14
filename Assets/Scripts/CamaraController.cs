using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;//Posicion de la camara

	void Start () {
		offset = transform.position - player.transform.position;//Inicia la posicion de la camara donde el jugador
	}
	
	// Actualiza despues de cada frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;//Cambia la posicion de la camara para que siga al jugador
	}
}
