using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	//Board position variables
	private int x, y;
	
	//Color lerping variables
	private SpriteRenderer render;
	private bool state = false;
	[Range(0, 1)]
	public float maxAlpha;
	public float time = 1;
	private float t = 0;
	private Color startColor;
	private Color endColor;

	private void Start () {
		render = GetComponent<SpriteRenderer>();
		startColor = new Color(render.color.r, render.color.g, render.color.b, 0);
		endColor = new Color(render.color.r, render.color.g, render.color.b, maxAlpha);
		render.color = startColor;

		string[] data = gameObject.name.Split(',');
		x = int.Parse(data[0]);
		y = int.Parse(data[1]);
	}

	private void Update () {
		//Color alpha interpolation
		if (state) {
			if (render.color.a != 1) {
				t += Time.deltaTime;
				if (t > 1) {
					t = 1;
				}

				render.color = Color.Lerp(startColor, endColor, t);
			}
			else {
				t = 0;
			}
		}
		else {
			if (render.color.a != 0) {
				t += Time.deltaTime;
				if (t > 1) {
					t = 1;
				}

				render.color = Color.Lerp(endColor, startColor, t);
			}
			else {
				t = 0;
			}
		}
	}

	private void OnMouseDown () {
		state = true;
	}

	private void OnMouseUp () {
		Game.TileClick(x, y, transform);
		state = false;
	}
}
