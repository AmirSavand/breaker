using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
	public float fadeMin = 0;
	public float fadeMax = 1;
	public float fadeRate = 0.1f;
	public float fadeAfter = 0;

	private SpriteRenderer spriteRenderer;
	private Image image;

	void Start ()
	{
		// Get inits
		spriteRenderer = GetComponent<SpriteRenderer> ();
		image = GetComponent<Image> ();

		// Start changing alpha
		InvokeRepeating ("changeAlpha", 0, fadeRate);
	}

	void changeAlpha ()
	{
		Color color = new Color ();

		// Original color
		if (spriteRenderer) {
			color = spriteRenderer.color;
		} else if (image) {
			color = image.color;
		}

		Color toColor = new Color (color.r, color.g, color.b, Mathf.Clamp (Random.value, fadeMin, fadeMax));

		// Change alpha color
		if (spriteRenderer) {
			spriteRenderer.color = toColor;
		} else if (image) {
			image.color = toColor;
		}
	}
}
