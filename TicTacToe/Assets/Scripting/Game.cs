using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public static bool finished = false; //Indicates if the game has finished (board full or a player has won)

	public static void TileClick (int x, int y, Transform t) {
		//Exits the function if the game has finished
		if (finished) { return; }

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

			//Checks the board state
			CheckState();

			//Updates turn
			turn = !turn;
		}
	}

	private static void CheckState () {
		//Checks if the baord is full
		bool full = CheckFull();
		
		//Checks if a player has won
		int winner = CheckWinner();
		if (winner != 0) {
			Win(winner);
		}

		//Checks if there's a draw
		if (winner == 0 && full) {
			Win(0);
		}
	}

	private static bool CheckFull () {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (board[i, j] == 0) {
					return false;
				}
			}
		}

		return true;
	}

	//Checks if a player has won, returns player's number
	private static int CheckWinner () {
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
		Text winnerText = GameObject.Find("Canvas").transform.Find("WinnerText").GetComponent<Text>();
		if (player == 0) {
			Debug.Log("It's a draw");
			winnerText.text = "Draw!";
		}
		else {
			Debug.Log("Player " + player + " has won");
			winnerText.text = "Player " + player + " has won!";
		}

		finished = true;
	}

}
