using UnityEngine;
using System.Collections;

//Clase que se ocupa del menu principal
public class MainMenu : MonoBehaviour {
	public Texture2D background;
	public Texture2D title;
	public Texture2D loading;
	private bool load = false;

	public MainMenu(){}

	//Metodo propio de UNity para iniciar la clase (una vez se ha construido)
	void Start(){
		//Screen configuration
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	//Metodo propio de Unity que actualiza la GUI
	void OnGUI () {
		if(load == false){
			//para ajustar
			GUILayout.BeginArea(new Rect (0, 0, Screen.width, Screen.height), background);
				GUILayout.FlexibleSpace();
					GUI.Label (new Rect ((Screen.width / 2)-200, (Screen.height / 2)-300, 400, 500), title);
				GUILayout.FlexibleSpace();
			GUILayout.EndArea();
			if (GUI.Button (new Rect ((Screen.width / 2) - 100, (Screen.height / 2)-100, 200, 75), "Museo")) {
				load =true;
				Application.LoadLevel("museumMode");		
			}
			if (GUI.Button (new Rect ((Screen.width / 2) - 100, (Screen.height / 2)+25, 200, 75), "Combate libre")) {
				load =true;
				Application.LoadLevel("combatMode");	
			}
		}else{
			GUILayout.BeginArea(new Rect (0, 0, Screen.width, Screen.height), loading);
			GUILayout.FlexibleSpace();
			GUILayout.FlexibleSpace();
			GUILayout.EndArea();
		}
	}
}
