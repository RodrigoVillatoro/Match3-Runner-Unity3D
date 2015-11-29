using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharacterPicker : MonoBehaviour {

	public static CharacterPicker instance = null;

	public int characterPicked = 0;
	public int maxCharacters; // Number of characters in the game
	
	public Mesh[] meshes;
	public Texture[] textures;


	void Awake() {

		// Singleton
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad(gameObject);

	}

	void Start() {

		Object[] charMats = Resources.LoadAll("Imported/core/fbx/");
		maxCharacters = charMats.Length/3; // Resourses contain: GameObject + Mesh + Texture, in that order, for the same Character. 
		
		meshes = new Mesh[maxCharacters];
		textures = new Texture[maxCharacters];
		
		int itemNum = 0;
		
		for (int i = 0; i < charMats.Length; i++) {
			
			if (((float)i)%3 == 1) {
				meshes[itemNum] = charMats[i] as Mesh;
			} else if (((float)i)%3 == 2) {
				textures[itemNum] = charMats[i] as Texture;
				++itemNum;
			}
			
		}

	}


}

