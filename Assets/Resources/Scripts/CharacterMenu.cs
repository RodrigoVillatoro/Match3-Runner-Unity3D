using UnityEngine;
using System.Collections;

public class CharacterMenu : MonoBehaviour {

	public GameObject characterAvatar;
	private int lastCharacter;

	int screenWidth;

	// Use this for initialization
	void Start () {
		screenWidth = Screen.width;
		lastCharacter = CharacterPicker.instance.maxCharacters - 1;
		RefreshCharacterAvatar();
	}
	
	// Update is called once per frame
	void Update () {

		// Unity editor, standalone, etc
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			NextCharacter();
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			PreviousCharacter();
		}

		// Mobile
		if (Input.touchCount > 0) {
			Touch myTouch = Input.touches[0];
			if (myTouch.phase == TouchPhase.Began) {
				if (myTouch.position.x > screenWidth/2) {
					NextCharacter();
				} else {
					PreviousCharacter();
				}
			}
		}

	}

	void NextCharacter() {

		if (CharacterPicker.instance.characterPicked == lastCharacter) {
			CharacterPicker.instance.characterPicked = lastCharacter;
		} else {
			++CharacterPicker.instance.characterPicked;
		}
		RefreshCharacterAvatar();
	}

	void PreviousCharacter() {
		if (CharacterPicker.instance.characterPicked == 0) {
			CharacterPicker.instance.characterPicked = 0;
		} else {
			--CharacterPicker.instance.characterPicked;
		}
		RefreshCharacterAvatar();
	}

	void RefreshCharacterAvatar() {
		characterAvatar.GetComponent<MeshFilter>().mesh = CharacterPicker.instance.meshes[CharacterPicker.instance.characterPicked];
		characterAvatar.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", CharacterPicker.instance.textures[CharacterPicker.instance.characterPicked]);
//		characterAvatar.GetComponent<MeshRenderer>().material.SetTexture(0, CharacterPicker.instance.textures[CharacterPicker.instance.characterPicked]);
	}

	public void StartGame() {
		Application.LoadLevel("1_MainGame");
	}

}
