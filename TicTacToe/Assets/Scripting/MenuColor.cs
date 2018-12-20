using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuColor : MonoBehaviour {

	private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
	private List<Image> uiImages = new List<Image>();

	/*private Vector3 a = Vector3.one;
	private Vector3 b = Vector3.zero;
	private Vector3 start = Vector3.zero;
	private Vector3 dir = Vector3.up;
	*/

	private Color a;
	private Color b;

	public Color start = (Color)(new Color32(56, 62, 70, 255));
	public Color end = Color.white;
	private int multiplier = 1;

	public AnimationCurve transition;
	public float time;

	private float t = 0;

	private void Start () {
		foreach (Transform t in GameObject.Find("Walls").transform) {
			sprites.Add(t.GetComponent<SpriteRenderer>());
		}
		foreach (Transform t in GameObject.Find("Canvas").transform) {
			uiImages.Add(t.GetComponent<Image>());
		}
	}

	private void Update () {
		/* EPILEPTIC VERSION OWO
		t += Time.deltaTime/2;
		a = Vector3.Lerp(start, dir, t);
		if (t >= 1) {
			t = 0;
			start = new Vector3(dir.x, dir.y, dir.z);
			
			if (dir == Vector3.up) 	  	   { dir = Vector3.right; }
			else if (dir == Vector3.right) { dir = Vector3.down;  }
			else if (dir == Vector3.down)  { dir = Vector3.left;  }
			else if (dir == Vector3.left)  { dir = Vector3.up;    }
		}
		*/

		//Normal Version
		t += (Time.deltaTime / time) * multiplier;
		a = Color.Lerp(start, end, transition.Evaluate(t));
		b = Color.Lerp(start, end, transition.Evaluate(1-t));

		if (t >= 1 || t <= 0) {
			multiplier *= -1;
		}

		Camera.main.backgroundColor = a;
		foreach (SpriteRenderer r in sprites) {
			r.color = b;
		}
		foreach (Image i in uiImages) {
			i.color = b;
		}
	}

	private Color VecToColor (Vector3 v) {
		return new Color(v.x, v.y, v.z, 1f);
	}
}
