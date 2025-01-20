using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	private int score; //For count score
	public Text scoreText; //For show score
	public GameObject newHighScoreText; //For show when player got high score
	public Text gameoverScoreText; //For show score when gameover
	public Text gameoverHighscoreText; //For show highscore when gameover
	public Canvas gameoverGUI; //For show GUI of gameover
	public Canvas ingameGUI;  //For show GUI when play (pause button ,scoreText)
	public Canvas pauseGUI; //For show GUI when pause
	public static GameController instance; //Instance
	public GameObject AdPanel;
	public bool invincible;
	public GameObject InvincibleObj;
	public bool isCancelRevive;

	void Awake(){
		instance = this;
	}

	public void CancelRevive()
    {
		SetAdPanel(false);
		isCancelRevive = true;
    }

	public void SetAdPanel(bool a)
	{
		AdPanel.SetActive(a);
		Time.timeScale = a == true ? 0 : 1;
	}

	public void ClickReviveBtn()
	{
		AdManager.ShowVideoAd("192if3b93qo6991ed0",
			(bol) => {
				if (bol)
				{
					SetAdPanel(false);
					invincible = true;
					InvincibleObj.SetActive(true);
					StartCoroutine("Invincible");
					
					AdManager.clickid = "";
					AdManager.getClickid();
					AdManager.apiSend("game_addiction", AdManager.clickid);
					AdManager.apiSend("lt_roi", AdManager.clickid);


				}
				else
				{
					StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
				}
			},
			(it, str) => {
				Debug.LogError("Error->" + str);
				//AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
			});

	}

	public  IEnumerator Invincible()
    {
		yield return new WaitForSeconds(3);
		invincible = false;
		InvincibleObj.SetActive(false);
	}

	public void addScore(){
		score++; //Plus score
		scoreText.text = score.ToString (); //Change scoreText to current score
	}

	public void GameOver(){
		CheckHighScore ();
		gameoverScoreText.text = score.ToString(); //Set score to gameoverScoreText
		gameoverHighscoreText.text = PlayerPrefs.GetInt ("highscore", 0).ToString(); //Set high score to gameoverHighscoreText
		gameoverGUI.gameObject.SetActive(true); //Show gameover's GUI
		ingameGUI.gameObject.SetActive(true); //Hide ingame's GUI

		AdManager.ShowInterstitialAd("1lcaf5895d5l1293dc",
			() => {
				Debug.LogError("--插屏广告完成--");

			},
			(it, str) => {
				Debug.LogError("Error->" + str);
			});
	}

	public void CheckHighScore(){
		if( score > PlayerPrefs.GetInt("highscore",0) ){ //If score > highscore
			PlayerPrefs.SetInt("highscore",score); //Save a new highscore
			newHighScoreText.gameObject.SetActive(true); //Enable newHighScoreText
		}
	}

	public void Pause(){
		Time.timeScale = 0; //Change timeScale to 0
		pauseGUI.gameObject.SetActive (true); //Show pauseGUI
	}

	public void Resume(){
		Time.timeScale = 1; //Change timeScale to 1
		pauseGUI.gameObject.SetActive (false); //Hide pauseGUI
	}
}
