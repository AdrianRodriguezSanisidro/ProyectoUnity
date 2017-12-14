using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;//Variable de la velocidad del jugador
	public float jumpForce=2.0f;//Variable de la fuerza de salto
	public Text winText;//Texto de victoria
	public Text countText;//Texto que cuenta los deployables recogidos
	public Text timeText;//Texto del tiempo transcurrido
	public GameObject player;//GameObject de jugador
	public Vector3 spawn;//Localizacion del spawn

	AudioSource fuenteAudio;//Audio que sonara al recojer el deployable

	public float tiempo;//Variable que recoge el tiempo transcurrido
	private Rigidbody rb;
	private int count;//Variable que recoge la cantidad de deployables cogidos
	private bool isGrounded;//Booleano que dice si el jugador esta en el suelo o en el aire
	private Vector3 jump=new Vector3(0.0f,2.0f,0.0f);//Vector de salto

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();//Le da los valores del Rigidbody a la variable rb
		fuenteAudio = GetComponent<AudioSource> ();//Le da el valor del AudioSource a la variable fuenteAudio
		tiempo = 0.0f;//Inicia la variable tiempo
		count = 0;//Inicia la variable count
		SetCountText ();//Llama al metodo
		winText.text = "";//Inicia el texto de victoria vacio
		timeText.text = "0";//Inica el tiempo en 0
	}

	void FixedUpdate ()
	{
		//Codigo para que el jugador se mueva(para movil)
		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.y;
		transform.position += new Vector3 (moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;
	}

	void Update()
	{
		//Codigo para saltar cuando se esta tocando el suelo
		if (Input.touchCount==1 && isGrounded) 
		{
			rb.AddForce (jump * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}

		tiempo = tiempo + 1 * Time.deltaTime;//Contador del tiempo en segungundos
		timeText.text = "Segundos: "+ (int)tiempo;//El tiempo se muestra en pantalla

		//Sales del juego al pulsar el boton de retroceso
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		}

		//Te devuelve al punto de spawn al estar por debajo de y=-6
		if (player.transform.position.y<-6)
		{
			player.transform.position = spawn;
		}

	}

	void OnTriggerEnter(Collider other) 
	{
		//Cuando el jugador choca con un Pick Up...
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);//El Pick Up desaparece
			fuenteAudio.Play ();//Se activa el sonido de fuenteAudio
			count = count + 1;//Suma 1 al count
			SetCountText ();//Actualiza el countText
		}
	}
	//Metodo para mostrar los deployables recogidos y mostrar la victoria al recogerlos todos
	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) 
		{
			winText.text = "You win!";
		}
	}
	//Metodo que marca cuando el jugador esta en el suelo
	void OnCollisionStay()
	{
		isGrounded = true;
	}
}