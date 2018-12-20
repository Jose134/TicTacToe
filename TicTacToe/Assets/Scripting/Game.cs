using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

	public static GameObject xPrefab; //Player 1
	public static GameObject oPrefab; //Player 2

	//Sets the board (0 = empty; 1 = player 1; -1 = player 2)
	public static int[,] board = {
		{ 0, 0, 0 },
		{ 0, 0, 0 },
		{ 0, 0, 0 }
	};

	public static bool turn = false; //false = Player1; true = Player 2

	public static void TileClick (int x, int y, Transform t) {
		//Checks if the tile is empty
		if (board[x, y] == 0) {
			if (turn) {
				//Sets board value for player 2
				board[x, y] = -1;
			}
			else {
				//Sets board value for player 1
				board[x, y] = 1;
			}

			//Instantiates an X or O GameObject
			GameObject go = GameObject.Instantiate(turn ? oPrefab : xPrefab);
			go.transform.position = t.position;
			go.transform.parent = t;

			//Checks if a player has won
			int winner = CheckBoard();
			if (winner != 0) {
				Win(winner);
			}

			//Updates turn
			turn = !turn;
		}
	}

	//Checks if a player has won, returns player's number
	private static int CheckBoard () {
		int sum = 0;

		//Checks columns
		for (int i = 0; i < 3; i++) {
			sum = 0;
			for (int j = 0; j < 3; j++) {
				sum += board[i, j];
			}

			if (sum == 3)  { return 1; }
			if (sum == -3) { return 2; }
		}

		//Checks rows
		for (int j = 0; j < 3; j++) {
			sum = 0;
			for (int i = 0; i < 3; i++) {
				sum += board[i, j];
			}

			if (sum == 3)  { return 1; }
			if (sum == -3) { return 2; }
		}

		//Checks diagonals
		sum = 0;
		for (int i = 0; i < 3; i++) {
			sum += board[i, i];

			if (sum == 3)  { return 1; }
			if (sum == -3) { return 2; }
		}

		sum = 0;
		for (int i = 0; i < 3; i++) {
			sum += board[i, 2-i];	

			if (sum == 3)  { return 1; }
			if (sum == -3) { return 2; }
		}

		return 0;
	}

	//Function called when a player wins
	private static void Win (int player) {
		Debug.Log("Player " + player + " has won");
	}

}
