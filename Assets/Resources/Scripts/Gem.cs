using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gem : MonoBehaviour {

	public List<Gem> neighbors = new List<Gem>();
	public GameObject sphere;

	private Board board;
	private GameObject container;
	private float gemVelocityX = -0.30f;
	private bool beenCatched = false;
	
	public bool bottomFeelerTouched = false;
	public string color;
	public bool isMatched = false;

	public int xCoord {
		get {
			return Mathf.RoundToInt(transform.localPosition.x);
		}
	}

	public int yCoord {
		get {
			return Mathf.RoundToInt(transform.localPosition.y);
		}
	}

	// Use this for initialization
	void Start () {
		board = GameObject.Find("Board").GetComponent<Board>();
		container = board.gameObject;
	}

	public bool IsNeighborWith(Gem gem) {
		if (neighbors.Contains(gem)) {
			return true;
		} else {
			return false;
		}
	}

	public void AddNeighbor(Gem gem) {
		if (!neighbors.Contains(gem)) {
			neighbors.Add(gem);
		}
	}

	public void RemoveNeighbor(Gem gem) {
		neighbors.Remove(gem);
	}

	void OnTriggerEnter(Collider col) {

		if (!beenCatched) {

			int column = 1;

			if (col.CompareTag("Character")) { 

				beenCatched = true;

				if (Board.first_ColumnList.Count < 4) {
					Board.first_ColumnList.Add(gameObject.GetComponent<Gem>());
					column = 1;
				} else if (Board.second_ColumnList.Count < 4) {
					Board.second_ColumnList.Add(gameObject.GetComponent<Gem>());
					column = 2;
				} else if (Board.third_ColumnList.Count < 4) {
					Board.third_ColumnList.Add(gameObject.GetComponent<Gem>());
					column = 3;
				} else if (Board.fourth_ColumnList.Count < 4) {
					Board.fourth_ColumnList.Add(gameObject.GetComponent<Gem>());
					column = 4;
				}

				gameObject.transform.SetParent(container.transform);
				gameObject.transform.localPosition = new Vector3((float)column, 4.0f, -3.0f);
				gameObject.GetComponent<Rigidbody>().useGravity = true;

				Board.catchedGemsList.Add(this);

			}

		}

		
	}

	void Update() {

		if (!beenCatched) {
			gameObject.transform.position += new Vector3(gemVelocityX, 0.0f, 0.0f);
		}


	}

	void FixedUpdate() {

		if (beenCatched) {

			if (gameObject.GetComponent<Rigidbody>().velocity.y <= -0.1) {

				if (neighbors.Count > 0 && bottomFeelerTouched) {
					board.CheckMatch(this);
				}

			}
		}

	}

	void OnDestroy() {
		Board.catchedGemsList.Remove(this);
	}


}
