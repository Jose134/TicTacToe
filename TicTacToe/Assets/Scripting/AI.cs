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

	private static Move GetBestMove (int[,] board, bool turn) {
		Move bestMove = new Move(-1, -1, turn ? 10000 : -10000);

		int winner = Game.CheckWinner(board);
		if (winner == 2) {
			return new Move(-1, -1, -10);
		}
		else if (winner == 1) {
			return new Move(-1, -1, 10);
		}

		List<Vector2> freeTiles = GetFreeTiles(board);

		List<Move> moves = new List<Move>();
		foreach (Vector2 tile in freeTiles) {
			board[(int)tile.x, (int)tile.y] = turn ? 2 : 1;
			
			Move next = GetBestMove(board, !turn);
			next.x = (int)tile.x;
			next.y = (int)tile.y;
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
			foreach (Move m in moves) {
				if (m.score > bestMove.score) {
					bestMove = m;
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
