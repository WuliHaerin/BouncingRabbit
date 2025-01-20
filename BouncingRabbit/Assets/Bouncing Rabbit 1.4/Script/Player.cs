using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Player : MonoBehaviour {

	[HideInInspector]
	public bool facingUp = true;	// For determining which way the player is currently facing.
	
	public float speed; //Speed of player
	public AudioClip deadSound; //Sound When player dead
	public AudioClip coinSound; //Coin Picker sound
	private Vector3 direction; //Direction of player
	private int fingerId = -1; //For every non-mobile Platform
	private bool isDead; //For check status of Player
	
	void Awake(){
		if (Application.isMobilePlatform) {
			fingerId = 0; //for mobile and unity
		}

		direction = Vector3.up; //When starting the game. Rabbit will go to the top first.
	}
	
	// Update is called once per frame
	void Update () {
	
		//When click and Player alive and not paused.
		if (Input.GetMouseButtonDown (0) && !isDead && Time.timeScale != 0) {

			//For Block click through UI 
			if(EventSystem.current.IsPointerOverGameObject(fingerId)){
				return; //Not Change Direction
			}

			ChangeDirection (); //Change Direction
		}

		transform.Translate (direction * speed * Time.deltaTime);
	}

	void ChangeDirection(){
		if (direction == Vector3.down) {
			direction = Vector3.up; //Change direction to down
		} else {
			direction = Vector3.down; //Change direction to up
		}
		Flip ();
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingUp = !facingUp;
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;
	}

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "Rope")
        {
			GetComponent<Rigidbody2D>().simulated = true;
            ChangeDirection(); //Change direction when it collides with the rope.
        }
    }



    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    //When Player collide with Obstacle while alive.
    //    if (coll.transform.tag == "Obstacle" && !isDead)
    //    {
    //        if (!GameController.instance.invincible)
    //        {
    //            GameController.instance.SetAdPanel(true);
    //            StartCoroutine("PreDead");
    //        }
    //    }
    //}


    public IEnumerator PreDead()
    {
		yield return new WaitForSeconds(0.1f);
		if (!GameController.instance.invincible)
		{
			Dead();
		}
	}

	public void Dead()
    {
		isDead = true;
		SoundManager.instance.PlaySingle(deadSound); //Play Dead sound
		GameController.instance.GameOver(); //Call Gameover
	}




	void OnTriggerEnter2D(Collider2D coll){
		//When Player trigger with Coin while alive.
		if (coll.transform.tag == "Coin" && !isDead) {
			Destroy(coll.gameObject); //Destroy Coin
			SoundManager.instance.PlaySingle(coinSound); //Play coin picker sound
			GameController.instance.addScore(); //Add score
			CoinSpawner.instance.SpawnCoin(); //Spawn a new coin
		}
		if (coll.transform.tag == "Obstacle" && !isDead)
		{
			if (!GameController.instance.invincible && !GameController.instance.isCancelRevive)
			{
				GameController.instance.SetAdPanel(true);
				StartCoroutine("PreDead");
			}
		}
	}

    private void OnDisable()
    {
		StopAllCoroutines();
    }




}
