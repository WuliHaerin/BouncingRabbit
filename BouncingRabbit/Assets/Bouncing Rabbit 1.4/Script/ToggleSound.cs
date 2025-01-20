using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleSound : MonoBehaviour {

	public Image soundButton;
	public Sprite soundOff,soundOn; 

	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetString ("sound", SoundConstants.soundOn) == SoundConstants.soundOff) {
			soundButton.GetComponent<Image>().sprite = soundOff;
		} else {
			soundButton.GetComponent<Image>().sprite = soundOn; 
		}
	}
}
