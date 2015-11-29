using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	private float velocity = 0.3f;

	// Update is called once per frame
	void Update () {
		float positionX = this.transform.position.x - velocity;
		this.transform.position = new Vector3(positionX, this.transform.position.y, this.transform.position.z);
	}

}
