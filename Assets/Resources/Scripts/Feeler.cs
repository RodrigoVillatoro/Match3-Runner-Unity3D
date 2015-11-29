using UnityEngine;
using System.Collections;

public class Feeler : MonoBehaviour {

	public Gem owner; 

	void OnTriggerEnter(Collider col) {

		if (col.CompareTag("Gem") || col.CompareTag("BottomWall")) {

			if (gameObject.CompareTag("BottomFeeler")) {
				Debug.Log("Hit Bottom!");
				owner.bottomFeelerTouched = true;
			}

			owner.AddNeighbor(col.GetComponent<Gem>());
		}

	}

	void OnTriggerExit(Collider col) {

		if (col.CompareTag("Gem")) {
			owner.RemoveNeighbor(col.GetComponent<Gem>());
		}

	}

}
