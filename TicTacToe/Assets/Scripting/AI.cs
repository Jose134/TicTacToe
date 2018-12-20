using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//thanks <3
//https://medium.freecodecamp.org/how-to-make-your-tic-tac-toe-game-unbeatable-by-using-the-minimax-algorithm-9d690bad4b37

public struct Move {
	public int x, y;
	public int score;

	public Move (int x, int y, int score) {
		this.x = x;
		this.y = y;
		this.score = score;
	}
};

public static class AI {

	public static void Play () {
		//Move owo = minimax(Game.board, 2);
		Move owo = GetBestMove(Game.board, true);
		Transform tile = GameObject.Find("Board").transform.Find("Tiles").Find(owo.x + "," + owo.y);
		Game.TileClick(owo.x, owo.y, tile);
	}

	/* private static Move minimax (int[,] board, int player) {

		int winner = Game.CheckWinner(board);
		if (winner == 1) {
			Move ret = new Move();
			ret.score = -10;
			return ret;
		}
		else if (winner == 2) {
			Move ret = new Move();
			ret.score = 10;
			return ret;
		}

		List<Vector2> freeTiles = GetFreeTiles(board);

		List<Move> moves = new List<Move>();
		for (int i = 0; i < freeTiles.Count; i++) {
			//Creates a move at the free tile's position
			Move move = new Move();
			move.position = freeTiles[i];

			//Sets the tile to the moving player
			board[(int)move.position.x, (int)move.position.y] = player;

			//Recursively iterates the minimax function
			if (player == 1) {
				Move result = minimax(board, 2);
				move.score = result.score;
			}
			else {
				Move result = minimax(board, 1);
				move.score = result.score;
			}

			//Resets the tile
			board[(int)move.position.x, (int)move.position.y] = 0;
			moves.Add(move);
		}


		Move bestMove = new Move();
		if (player == 2) {
			var bestScore = -10000;
			for (int i = 0; i < moves.Count; i++) {
				if (moves[i].score > bestScore) {
					bestScore = moves[i].score;
					bestMove = moves[i];
				}
			}
		}
		else if (player == 1) {
			var bestScore = 10000;
			for (int i = 0; i < moves.Count; i++) {
				if (moves[i].score < bestScore) {
					bestScore = moves[i].score;
					bestMove = moves[i];
				}
			}
		}

		return bestMove;
	}*/

	private static Move GetBestMove (int[,] board, bool turn) {
		Move bestMove = new Move(1, 1, 0);

		int winner = Game.CheckWinner(board);
		if (winner == 2) {
			return new Move(1, 1, -10);
		}
		else if (winner == 1) {
			return new Move(1, 1, 10);
		}

		List<Vector2> freeTiles = GetFreeTiles(board);

		List<Move> moves = new List<Move>();
		foreach (Vector2 tile in freeTiles) {
			board[(int)tile.x, (int)tile.y] = turn ? 2 : 1;
			
			Move next = GetBestMove(board, !turn);
			moves.Add(next);
			
			board[(int)tile.x, (int)tile.y] = 0;
		}

		if (turn) {
			foreach (Move m in moves) {
				if (m.score < bestMove.score) {
					bestMove = m;
				}
			}
		}
		else {
			if (turn) {
			foreach (Move m in moves) {
				if (m.score > bestMove.score) {
					bestMove = m;
				}
			}
		}
		}

		return bestMove;
	}

	private static List<Vector2> GetFreeTiles (int[,] board) {
		List<Vector2> tiles = new List<Vector2>();

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (board[i, j] == 0) {
					tiles.Add(new Vector2(i, j));
				}
			}
		}

		return tiles;
	}
}
