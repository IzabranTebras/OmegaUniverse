using UnityEngine;
using System.Collections;

//Clase encargada del modo museo
public class MuseumGUI : MonoBehaviour {
	public Texture2D camara = null;
	public Texture2D menu = null;
	public GUISkin button = null;
	public AudioSource audio = null;
	private bool showGUI = true;

	public MuseumGUI(){}

	//Metodo propio de Unity que actualiza la GUI
	void OnGUI () {
		GUI.skin = button;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		if (showGUI) {
			if (GUI.Button (new Rect (Screen.width - 140, (Screen.height / 2) - 65, 130, 130), camara)) {
				showGUI = false;
				Application.CaptureScreenshot (System.DateTime.Now.Ticks + "ScreenShot.png");
				audio.Play();
				StartCoroutine(Pause());
			}
			if (GUI.Button (new Rect (20, 20, 120, 120), menu)) {
				Application.LoadLevel ("mainMenu");
			}
		}
	}
	IEnumerator Pause() {
		yield return new WaitForSeconds(1);  //yield asegura que la ejecucion continue desde aqui cuando vuelva a ejecutarse
		showGUI = true;		//Cuando ha pasado el tiempo (1 segundo) vuelven a aparecer los botones
	}
}
