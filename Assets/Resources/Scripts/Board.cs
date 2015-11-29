using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

	public static List<Gem> first_ColumnList = new List<Gem>();
	public static List<Gem> second_ColumnList = new List<Gem>();
	public static List<Gem> third_ColumnList = new List<Gem>();
	public static List<Gem> fourth_ColumnList = new List<Gem>();

	public static List<Gem> catchedGemsList = new List<Gem>();
	
	private const int GEMS_TO_MATCH = 3;
	public bool isMatched = false;
	
	public void CheckMatch(Gem catchedGem) {


		List<Gem> gem1List = new List<Gem>();
		ConstructMatchList(catchedGem.color, catchedGem, catchedGem.xCoord, catchedGem.yCoord, ref gem1List);

		int gemsToMatch = GEMS_TO_MATCH; 

		FixMatchList(catchedGem, gem1List, gemsToMatch);

		if (IsGameOver()) {
			CleanAndRestartGame();
		}

	}

	public void ConstructMatchList(string color, Gem gem, int xCoord, int yCoord, ref List<Gem> MatchList) {

		if (gem == null) {
			return;
		} else if (gem.color != color) {
			return;
		} else if (MatchList.Contains(gem)) {
			return;
		} else {
			MatchList.Add(gem);
			if (xCoord == gem.xCoord || yCoord == gem.yCoord) {
				foreach (Gem g in gem.neighbors) { 	
					ConstructMatchList(color, g, xCoord, yCoord, ref MatchList);
				}
			}
		}

	}

	public void FixMatchList(Gem gem, List<Gem> ListToFix, int amountToMatch) {

		List<Gem> rows = new List<Gem>();
		List<Gem> columns = new List<Gem>();

		for (int i = 0; i < ListToFix.Count; i++) {
			if (gem.xCoord == ListToFix[i].xCoord) {
				rows.Add(ListToFix[i]);
			}
			if (gem.yCoord == ListToFix[i].yCoord) {
				columns.Add(ListToFix[i]);
			}
		}

		if (rows.Count >= amountToMatch) {
			isMatched = true;
			for (int i = 0; i < rows.Count; i++) {
				rows[i].isMatched = true;
			}
		}
		
		if (columns.Count >= amountToMatch) {
			isMatched = true;
			for (int i = 0; i < columns.Count; i++) {
				columns[i].isMatched = true;
			}
		}

		if (isMatched) {

			for(int i = 0; i < catchedGemsList.Count; i++) {

				if (catchedGemsList[i].isMatched) {

					if (first_ColumnList.Contains(catchedGemsList[i])) {
						first_ColumnList.Remove(catchedGemsList[i]);
					} else if (second_ColumnList.Contains(catchedGemsList[i])) {
						second_ColumnList.Remove(catchedGemsList[i]);
					} else if (third_ColumnList.Contains(catchedGemsList[i])) {
						third_ColumnList.Remove(catchedGemsList[i]);
					} else if (fourth_ColumnList.Contains(catchedGemsList[i])) {
						fourth_ColumnList.Remove(catchedGemsList[i]);
					} 

					Destroy(catchedGemsList[i].gameObject);

				}
			}

			
			isMatched = false;

		}

	}

	void CleanAndRestartGame() {

		// Clear all (static) lists
		catchedGemsList.Clear();
		first_ColumnList.Clear();
		second_ColumnList.Clear();
		third_ColumnList.Clear();
		fourth_ColumnList.Clear();
		
		Application.LoadLevel("0_CharMenu");

	}

	bool IsGameOver() {

		if ((first_ColumnList.Count == 4) && 
		    (second_ColumnList.Count == 4) && 
		    (third_ColumnList.Count == 4) && 
		    (fourth_ColumnList.Count == 4)) {
			return true;
		} else {
			return false;
		}

	}
	
}
