using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnValue{
	public float yPosition; //y Position for spawn
	public float [] xPosition; // x Position for spawn 
}

public class ObstacleSpawner : MonoBehaviour {

	public GameObject [] obstacle; //GameObject of Obstacle
	public SpawnValue spawnValue; //Spawn Position
	public float spawnWait; //Wait time for spawn

	public static ObstacleSpawner instance; //Instance

	void Awake(){
		instance = this;
	}
	
	void Start ()
	{
		StartCoroutine (SpawnWaves ()); //Start IEnumerator function
	}
	
	IEnumerator SpawnWaves ()
	{
		while (true)
		{
			int randomXPosition = Random.Range(0,spawnValue.xPosition.Length); //random Position Left or Right
			Vector3 spawnPosition = new Vector3 (spawnValue.xPosition[randomXPosition], Random.Range (-spawnValue.yPosition , spawnValue.yPosition ), 0.0f); //Position for spawn
			Quaternion spawnRotation = Quaternion.identity;

			Instantiate (obstacle[Random.Range(0,obstacle.Length)], spawnPosition, spawnRotation);
			yield return new WaitForSeconds (spawnWait); //Wait time
		}
	}
}
