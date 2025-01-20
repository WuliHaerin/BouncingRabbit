using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoundConstants{
	public const string soundOn = "on";
	public const string soundOff = "off";
}

public class SoundManager : MonoBehaviour {

	public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
	public static SoundManager instance = null; 
	
	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

	void Update(){
		if (PlayerPrefs.GetString ("sound",SoundConstants.soundOn) == SoundConstants.soundOff) { //If SoundOff
			musicSource.mute = true; //Mute
			efxSource.mute = true; //Mute
		} else { //Else sound is On
			musicSource.mute = false; // Note Mute
			efxSource.mute = false; //Note Mute
		}
	}

	public void PlaySingle(AudioClip clip)
	{
		//Set the clip of our efxSource audio source to the clip passed in as a parameter.
		efxSource.clip = clip;
		
		//Play the clip.
		efxSource.Play ();
	}

	public void Mute(){
		if (PlayerPrefs.GetString ("sound") == SoundConstants.soundOff) {
			PlayerPrefs.SetString("sound", SoundConstants.soundOn); //Save to Sound On
		} else {
			PlayerPrefs.SetString("sound",SoundConstants.soundOff); //Save to Sound Off
		}
	}
}
