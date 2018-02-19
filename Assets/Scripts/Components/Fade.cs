using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
	public float fadeMin = 0;
	public float fadeMax = 1;
	public float fadeRate = 0.1f;
	public float fadeAfter = 0;

	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		// Get inits
		spriteRenderer = GetComponent<SpriteRenderer> ();

		// Start changing alpha
		InvokeRepeating ("changeAlpha", 0, fadeRate);
	}

	void Update ()
	{
	}

	void changeAlpha ()
	{
		// Original color
		Color color = spriteRenderer.color;

		// Change alpha color
		spriteRenderer.color = new Color (color.r, color.g, color.b, Mathf.Clamp (Random.value, fadeMin, fadeMax));
	}
}
