using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customization : MonoBehaviour {

	public GameObject xPrefab;
	public GameObject oPrefab;
	public Color bgColor;

	private void Start () {
		Apply();
	}

	private void Apply () {
		Camera.main.backgroundColor = bgColor;

		Game.xPrefab = xPrefab;
		Game.oPrefab = oPrefab;
	}
}
