using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject gem;
	public static string[] gemMaterials = {"Red", "Blue", "Green", "Orange", "Yellow"};

	float randomTime {
		get {

			switch (gameObject.tag) {
				
			case "BottomSpawner":
				return 0;
			case "MidSpawner":
				return Random.Range(1,4);
			case "TopSpawner":
				return Random.Range(1,3);
			default: 
				return Random.Range(1,4);

			}

		}
	}

	void Start() {

		Invoke("CreateGem", randomTime);

	}

	void CreateGem() {

		GameObject gemGameObject = Instantiate(gem, gameObject.transform.position, Quaternion.identity) as GameObject; 
		Gem g = gemGameObject.GetComponent<Gem>();
		g.color = gemMaterials[Random.Range (0, gemMaterials.Length)];
		Material mat = Resources.Load("Materials/" + g.color) as Material;
		g.sphere.GetComponent<Renderer>().material = mat;

		Invoke("CreateGem", randomTime);

	}

}
