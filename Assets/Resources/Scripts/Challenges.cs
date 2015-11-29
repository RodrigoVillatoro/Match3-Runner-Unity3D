using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Challenges : MonoBehaviour {

	public string[] challenges;

	private int[] catchesArray = {5, 10, 15};
	private string[] gemArray;

	void Awake() {

		gemArray = Spawner.gemMaterials;

	}


	// Use this for initialization
	void Start () {

		challenges = new string[100];

		int index = 0;

		// Avoid
		for (int j = 0; j < gemArray.Length; j++) {
			challenges[index] = "Avoid " + gemArray[j].ToLower() + " gems and fill the entire board.";
			index++;
		}

		// Catch
		for (int i = 0; i < catchesArray.Length; i++) {
			for (int j = 0; j < gemArray.Length; j++) {
				challenges[index] = "Catch " + catchesArray[i] + " sets of " + gemArray[j].ToLower() + " gems.";
				index++;
			}
		}

		// Avoid and Catch
		for (int i = 0; i < catchesArray.Length; i++) {
			for (int j = 0; j < gemArray.Length; j++) {
				for (int k = 0; k < gemArray.Length; k++) {
					if (gemArray[j] == gemArray[k]) {
						continue;
					} else {
						challenges[index] = "Avoid " + gemArray[j].ToLower() + " gems and catch " + catchesArray[i] + " sets of " + gemArray[k].ToLower() + " gems.";
						index++;
					}
				}
			}
		}


		foreach (string ch in challenges) {
			Debug.Log(ch);
		}


	}


	
	// Update is called once per frame
	void Update () {
	
	}


}
