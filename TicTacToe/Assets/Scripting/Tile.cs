using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	private int x, y;

	private void Start () {
		string[] data = gameObject.name.Split(',');
		x = int.Parse(data[0]);
		y = int.Parse(data[1]);
	}

	private void OnMouseUp () {
		Game.TileClick(x, y, transform);
	}
}
