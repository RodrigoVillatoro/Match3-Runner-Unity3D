using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public GameObject characterAvatar;

	float squishRate = 0.85f;

	int screenWidth;
	Animator animator;
	bool isFlying;
	Vector3 goScale;

	float jumpForce = 800.0f;
	float horizontalForce = 100.0f;

	void Awake () {

		screenWidth = Screen.width;
		animator = GetComponent<Animator>();

	}

	void Start() {

		characterAvatar.GetComponent<MeshFilter>().mesh = CharacterPicker.instance.meshes[CharacterPicker.instance.characterPicked];
		characterAvatar.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", CharacterPicker.instance.textures[CharacterPicker.instance.characterPicked]);

		goScale = this.transform.localScale;

	}

	void OnTriggerEnter(Collider col) {


		if (col.CompareTag("Floor")) {
			animator.SetBool("OnAir", false);
		}

		if (col.CompareTag("Flying")) {
			animator.SetBool("OnAir", true);
		}


	}

	IEnumerator Squish() {


		this.transform.localScale = new Vector3(goScale.x, goScale.y * squishRate, goScale.z);

		yield return new WaitForSeconds(0.1f);

		this.transform.localScale = goScale;

	}

	void Update() {

		bool touched = false;

		// Unity editor, standalone, etc
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			horizontalForce = Mathf.Abs(horizontalForce);
			touched = true;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			horizontalForce = -Mathf.Abs(horizontalForce);
			touched = true;
		}
		
		// Mobile
		if (Input.touchCount > 0) {
			Touch myTouch = Input.touches[0];
			if (myTouch.phase == TouchPhase.Began) {
				if (myTouch.position.x > screenWidth/2) {
					horizontalForce = Mathf.Abs(horizontalForce);
					touched = true;
				} else {
					horizontalForce = -Mathf.Abs(horizontalForce);
					touched = true;
				}
			}
		}

		if (touched) {

			StartCoroutine(Squish());

			// Reset y velocity
			GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
			horizontalForce = 0.0f;
			// Double jump
			GetComponent<Rigidbody>().AddForce(new Vector3(horizontalForce, jumpForce, 0.0f));
		}


	}


}
