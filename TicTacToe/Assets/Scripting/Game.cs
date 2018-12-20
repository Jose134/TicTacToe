using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

	//Sets the board
	public static byte[,] board = {
		{ 0, 0, 0 },
		{ 0, 0, 0 },
		{ 0, 0, 0 }
	};

	public static bool turn = false; //false = Player1; true = Player 2

	public static void TileClick (int x, int y) {
		Debug.Log("Tile (" + x + "," + y + ") was clicked");
	}

}
