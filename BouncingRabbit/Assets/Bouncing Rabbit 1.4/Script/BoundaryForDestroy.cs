using UnityEngine;
using System.Collections;

public class BoundaryForDestroy : MonoBehaviour {

	void OnTriggerExit2D(Collider2D coll){
		Destroy (coll.gameObject); //Destroy 
	}
}
