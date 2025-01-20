using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

	public GameObject coin; //Coin object
	public float yPosition; //Position for spawn
	private bool isTop; //For check last position of coin

	public static CoinSpawner instance;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	void Start(){
		SpawnCoin (); //Spawn coin to top when start
	}

	public void SpawnCoin(){
		GameObject c = Instantiate (coin) as GameObject; //Spawn Coin

		if (isTop) {
			c.transform.position = new Vector2 (0.0f, -yPosition); //Spawn down Coin
		} else {
			c.transform.position = new Vector2 (0.0f, yPosition); //Spawn Top Coin
		}
		isTop = !isTop; 
	}
}
