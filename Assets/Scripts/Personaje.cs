using UnityEngine;
using System.Collections;

public class Personaje{
	//Atributos
	private string nombre;
	private string clan;
	private string elemento;
	private float vit;
	private float pm;
	private float ph;
	private float velocidad;
	private float atqM;
	private float atqF;
	private float defM;
	private float defF;

	//Constructors
	public Personaje(){}

	public Personaje(string nombre, string clan, string elemento, float vit, float pm, float ph, float velocidad, float atqM, float atqF, float defM, float defF){
		this.nombre = nombre;
		this.clan = clan;
		this.elemento = elemento;
		this.vit = vit;
		this.pm = pm;
		this.ph = ph;
		this.velocidad = velocidad;
		this.atqM = atqM;
		this.atqF = atqF;
		this.defM = defM;
		this.defF = defF;
	}

	//Getters and setters
	public string Nombre{
		get{
			return this.nombre;
		}
	}

	public string Clan{
		get{
			return this.clan;
		}
	}

	public string Elemento{
		get{
			return this.elemento;
		}
	}

	public float Vit{
		get{
			return this.vit;
		}
		set{
			this.vit = value;
		}
	}

	public float Pm{
		get{
			return this.pm;
		}
		set{
			this.pm = value;
		}
	}
	
	public float Ph{
		get{
			return this.ph;
		}
		set{
			this.ph = value;
		}
	}

	public float Velocidad{
		get{
			return this.velocidad;
		}
	}

	public float AtqM{
		get{
			return this.atqM;
		}
	}

	public float AtqF {
		get {
			return this.atqF;
		}
	}

	public float DefM {
		get {
			return this.defM;
		}
	}

	public float DefF{
		get{
			return this.defF;
		}
	}

	//Funcion que se encarga del ataque de un personaje. 
	//Recibe al personaje enemigo para poder conseguir su atributo de defensa y hacer el calculo del daño
	public int Ataque(Personaje enemy){
		int damage;
		float rand;

		rand = Random.Range (1.0f, 1.3f);
		damage = (int)(((atqF / enemy.DefF) * 150)*rand);
		return damage;
	}
}
