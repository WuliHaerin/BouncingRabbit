using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;	// For determining which way the obstacle is currently facing.
	public float speed;

	void Start(){

		//Value for Check It's spawn from left or right and for destroy it.
		float minXPosition = ObstacleSpawner.instance.spawnValue.xPosition [0];  //Get value of xPosition in SpawnValue of ObstacleSpawner

		if (transform.position.x <= minXPosition ) { //If position of obstacle < minXPosition It mean spawn from left (this value = -10)
			GetComponent<Rigidbody2D> ().velocity = speed * Vector3.right; //Move to Right side
		} else { //It spawn from right
			GetComponent<Rigidbody2D> ().velocity = speed * Vector3.left; //Move to Left side
			Flip();
		}
	}

	void Flip(){
		// Switch the way the obstacle is labelled as facing.
		facingRight = !facingRight;
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}