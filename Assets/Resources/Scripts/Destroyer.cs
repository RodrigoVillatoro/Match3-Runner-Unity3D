using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {


	void OnTriggerEnter(Collider col) {
		
		if (col.CompareTag("Gem")) {

			Destroy(col.gameObject);

		}
		
	}

}
