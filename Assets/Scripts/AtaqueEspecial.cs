using UnityEngine;
using System.Collections;

public class AtaqueEspecial {
	//Atributos
	protected string nombre;
	protected int coste;

	//Constructors
	public AtaqueEspecial(){}

	public AtaqueEspecial(string nombre, int coste){
		this.nombre = nombre;
		this.coste = coste;
	}

	//Getters and setters
	public string Nombre{
		get{
			return this.nombre;
		}
	}

	public int Coste{
		get{
			return this.coste;
		}
	}

}

//Clase que hereda de Ataque Especial, añadiendole el atributo de elemento y de bonificador, propios de un hechizo.
public class Hechizo : AtaqueEspecial{
	//Atributos
	private string elemento;
	private int bonificador;

	//Constructors
	public Hechizo(){}

	public Hechizo(string nombre, int coste, string elemento, int bonificador){
		this.nombre = nombre;
		this.coste = coste;
		this.elemento = elemento;
		this.bonificador = bonificador;
	}

	//Getters and setters
	public string Elemento{
		get{
			return this.elemento;
		}
	}
	
	public int Bonificador{
		get{
			return this.bonificador;
		}
	}
}