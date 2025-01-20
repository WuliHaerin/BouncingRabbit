using UnityEngine;
using System.Collections;

public class StartMenu_ButtonFunctions : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		//If touch back on mobile
		if (Input.GetKeyDown (KeyCode.Escape)) { 
			Application.Quit (); //Quit 
		}
	}
	
	public void Rate(){
		//Put your URL
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.example"); 
	}
	
	public void Play(){
		Application.LoadLevel ("Game"); //Load Game Scene
	}
	
	public void Mute(){
		SoundManager.instance.Mute (); //Mute
	}
}
