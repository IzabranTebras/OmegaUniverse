using System.Xml;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;

//Clase que se ocupa de la logica del combate
public class Combat : MonoBehaviour {
	
	public Texture2D menu = null;
	public GUISkin Button = null;
	public GameObject monster = null;
	public GameObject monster2 = null;
	public GameObject ice = null;
	public GameObject stalagmita = null;
	public GameObject VIT = null;
	public GameObject VIT2 = null;
	private float timer = 2.0f;
	private Personaje character;
	private Personaje enemy;
	private bool showGUI = false;
	private bool enemyAttack = false;
	private bool endBattle = false;
	public Texture2D endBackground = null;
	public Texture2D title = null;
	public Text guiDamage = null;
	private float damage;		//le pongo float para poder hacer la division

	//Constructores de la clase Combate
	public Combat(){
		character = new Personaje ("Froyo", "Agua", "Ninguno", 50.0f, 30.0f, 10.0f, 30.0f, 60.0f, 30.0f, 20.0f, 40.0f);
		enemy = new Personaje ("Cyclope", "Tierra", "Ninguno", 500.0f, 30.0f, 10.0f, 30.0f, 60.0f, 30.0f, 20.0f, 40.0f);

		//character = CargaAtributos ("Froyo");
		//enemy = CargaAtributos("Cyclope");
		}

	//Metodo propio de UNity para iniciar la clase (una vez se ha construido)
	void Start(){
		monster.GetComponent<Animation>().Play ("Apparition");
		guiDamage.enabled = false;
	}

	//Metodo propio de Unity que actualiza la GUI
	void OnGUI () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	
		//Escena A (Diagrama de flujo)
		if ((timer <= 0.0f) && (!monster.GetComponent<Animation>().isPlaying) && (enemyAttack==false)) {
			guiDamage.enabled = false;
			guiDamage.color = Color.red;
			monster.GetComponent<Animation>().Play ("LoopMove");
			showGUI=true;
		}

		//Los botones los muestra cuando te ha llegado el turno
		//Escena A (Diagrama de flujo)
		if(enemy.Vit>0){
			if (showGUI == true ){
				if (GUI.Button (new Rect (30, (Screen.height) - 100, (Screen.width/4)-5, 100),"Ataque")) {
					showGUI=false;
					monster.GetComponent<Animation>().Play("NormalAttack");
					damage = character.Ataque(enemy);
					enemy.Vit = enemy.Vit - damage;
					guiDamage.text = damage.ToString();
					timer=5.0f;
				}
				
				if (GUI.Button (new Rect ((Screen.width/4)+35, (Screen.height) - 100, (Screen.width/4)-5, 100),"Hechizo")) {
					showGUI=false;
					monster.GetComponent<Animation>().Play("SuperAttack");
					damage = character.Ataque(enemy);
					enemy.Vit = enemy.Vit - damage;
					guiDamage.text = damage.ToString();
					timer=5.0f;
				}
				
				if (GUI.Button (new Rect ((Screen.width/2)+40, (Screen.height) - 100, (Screen.width/4)-5, 100),"Habilidad")) {
					showGUI=false;
					monster.GetComponent<Animation>().Play("SuperAttack");
					damage = character.Ataque(enemy);
					enemy.Vit = enemy.Vit - damage;	
					guiDamage.text = damage.ToString();
					timer=5.0f;
				}

				//Boton del main menu
				GUI.skin = Button;
				if (GUI.Button (new Rect (20, 20, 120, 120), menu)) {
					Application.LoadLevel ("mainMenu");
				}
			}
		}else{
			//Pantalla de fin de batalla
			if (timer<=-1.0f){
				GUI.Box(new Rect (0, 0, Screen.width, Screen.height), endBackground);
				GUI.Label (new Rect ((Screen.width / 4), (Screen.height / 4), 400, 200), title);

				//Vuelve al main menu
				if (GUI.Button (new Rect ((Screen.width / 2) - 270, (Screen.height / 2)+100, 200, 75), "Salir")) {
					Application.LoadLevel("mainMenu");		
				}

				//Vuelve a combatir
				if (GUI.Button (new Rect ((Screen.width / 2) + 50, (Screen.height / 2)+100, 200, 75), "Repetir combate")) {
					Application.LoadLevel("combatMode");	
				}
			}
		}
	}

	//Metodo propio de Unity que actualiza la logica del programa
	void Update(){
		//Calculamos el cateto b
		//b = znew - zold
		float b = monster2.transform.position.z - monster.transform.position.z;

		//Calculamos el cateto a
		//a = xnew - xold
		float a = monster2.transform.position.x - monster.transform.position.x;

		Debug.Log(monster.transform.localRotation.y);
		Debug.Log(monster2.transform.eulerAngles.y);
		//actualizacion de timer
		if (timer > -1.0f) {
			timer -= Time.deltaTime;
		}

		//contraataque del enemigo
		if ((enemyAttack == true) && (timer <= 0.0f) && (enemy.Vit > 0)) {
			guiDamage.enabled = false;
			ice.SetActive(false);
			randomEnemyAttack();
			enemyAttack=false;
		}

		//Muerte del enemigo
		if ((enemy.Vit <= 0) && (endBattle == false) && (timer < 0.8f)) {
			monster2.GetComponent<Animation>().Play("death");
			VIT2.SetActive(false);
			guiDamage.enabled = true;
			endBattle = true;
		}

		//Ataque normal
		if ((monster.GetComponent<Animation>().IsPlaying ("NormalAttack")) && (timer < 1.5f) && (enemyAttack==false)) {
			ice.SetActive(true);
			ice.GetComponent<Animation>().Play("ice");

			//para calcular cuanto tiene que bajar la barra de salud
			if (damage<(enemy.Vit+damage)){		//aqui enemy.Vit ya tiene restado el damage, por eso comprobamos como estaba la vida antes
				VIT2.transform.localScale = new Vector3 (VIT2.transform.localScale.x, VIT2.transform.localScale.y- ((damage/enemy.Vit)*VIT2.transform.localScale.y), VIT2.transform.localScale.z);
			}else{
				VIT2.transform.localScale = new Vector3 (VIT2.transform.localScale.x, 0, VIT2.transform.localScale.z);
			}
			enemyAttack=true;
		}

		//Super ataque
		if ((monster.GetComponent<Animation>().IsPlaying ("SuperAttack")) && (timer < 1.5f) && (enemyAttack==false)) {
			stalagmita.GetComponent<Animation>().Play("stalagmita");
			
			//para calcular cuanto tiene que bajar la barra de salud
			if (damage<(enemy.Vit+damage)){		//aqui enemy.Vit ya tiene restado el damage, por eso comprobamos como estaba la vida antes
				VIT2.transform.localScale = new Vector3 (VIT2.transform.localScale.x, VIT2.transform.localScale.y- ((damage/enemy.Vit)*VIT2.transform.localScale.y), VIT2.transform.localScale.z);
			}else{
				VIT2.transform.localScale = new Vector3 (VIT2.transform.localScale.x, 0, VIT2.transform.localScale.z);
			}
			enemyAttack=true;
		}

		//Golpe recibido
		if (((monster.GetComponent<Animation>().IsPlaying ("NormalAttack")) || (monster.GetComponent<Animation>().IsPlaying ("SuperAttack")))&& (timer < 1.0f) && (enemyAttack==true) && (enemy.Vit>0)) {
			monster2.GetComponent<Animation>().Play("hit");
			guiDamage.enabled = true;
		}
	}

	//Funcion que se encarga de hacer una aleatorizacion de los 2 ataques que posee el enemigo y ataque
	public void randomEnemyAttack(){
		int aux = Random.Range (0, 1);
		
		if (aux == 0) {
			monster2.GetComponent<Animation>().Play ("attack_1");
		} else {
			monster2.GetComponent<Animation>().Play("attack_2");		
		}

		//Froyo lo esquiva
		if (monster.GetComponent<Animation>().isPlaying == false) {
			guiDamage.color = Color.green;
			guiDamage.text = "¡Ha fallado!";
			guiDamage.enabled = true;
			monster.GetComponent<Animation>().Play ("Defense");
		}
	}

	//Funcion que recibe el nombre de un personaje y lo busca en el XML, posteriormente carga sus atributos creando
	//al nuevo personaje. Si no es el fichero adecuado o no existe el personaje devuelve una excepcion.
	public Personaje CargaAtributos(string nombre){
		Personaje personaje = new Personaje();
		XmlDocument xDoc = new XmlDocument();
		try{
			xDoc.Load("Assets/BD.xml");
		}catch(XmlException e){
			Debug.LogException(e, this);
		}
		
		XmlNodeList personajes = xDoc.GetElementsByTagName("Personajes");
		XmlNodeList lista = 
			((XmlElement)personajes[0]).GetElementsByTagName("Personaje"); 
		
		int i=0;
		bool found = false;
		while ((i<lista.Count) && (found==false)) {
			XmlElement nodo = (XmlElement)lista[i];
			XmlNodeList nNombre = nodo.GetElementsByTagName ("Nombre");
			
			//en caso de que el nombre del luchador sea el que se busca, se cogen el resto de atributos para crear al personaje
			if(nNombre[0].InnerText.Equals(nombre)){
				XmlNodeList nClan = nodo.GetElementsByTagName ("Clan");
				XmlNodeList nElemento = nodo.GetElementsByTagName ("Elemento");
				XmlNodeList nVIT = nodo.GetElementsByTagName ("VIT");
				XmlNodeList nPM = nodo.GetElementsByTagName ("PM");
				XmlNodeList nPH = nodo.GetElementsByTagName ("PH");
				XmlNodeList nVelocidad = nodo.GetElementsByTagName ("Velocidad");
				XmlNodeList nATQM = nodo.GetElementsByTagName ("ATQM");
				XmlNodeList nATQF = nodo.GetElementsByTagName ("ATQF");
				XmlNodeList nDEFM = nodo.GetElementsByTagName ("DEFM");
				XmlNodeList nDEFF = nodo.GetElementsByTagName ("DEFF");
				personaje = new Personaje(nNombre[0].InnerText, nClan[0].InnerText, nElemento[0].InnerText, 
				                          float.Parse(nVIT[0].InnerText), float.Parse(nPM[0].InnerText), float.Parse(nPH[0].InnerText),
				                          float.Parse(nVelocidad[0].InnerText), float.Parse(nATQM[0].InnerText), float.Parse(nATQF[0].InnerText),
				                          float.Parse(nDEFM[0].InnerText), float.Parse(nDEFF[0].InnerText));
				found =true;
			}else{
				i++;
			}
		}
		if (found==false){
			throw new MissingComponentException("No existe el luchador");
		}
		return personaje;
	}
}
