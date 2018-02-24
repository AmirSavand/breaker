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

	public bool fadeSmooth = false;
	public Color fadeSmoothToColor;

	private Color color;
	private SpriteRenderer spriteRenderer;
	private Image image;

	void Start ()
	{
		// Get inits
		spriteRenderer = GetComponent<SpriteRenderer> ();
		image = GetComponent<Image> ();
		color = spriteRenderer ? spriteRenderer.color : image.color;

		// Start changing alpha
		if (!fadeSmooth) {
			InvokeRepeating ("changeAlpha", fadeAfter, fadeRate);
		}
	}

	void Update ()
	{
		// Fade toggle smooth color
		if (fadeSmooth) {
			setColor (Color.Lerp (color, fadeSmoothToColor, Mathf.PingPong (Time.time, 1)));
		}
	}

	void changeAlpha ()
	{
		// Change color with new alpha
		setColor (new Color (color.r, color.g, color.b, Mathf.Clamp (Random.value, fadeMin, fadeMax)));
	}

	void setColor (Color colorToSet)
	{
		// For sprite
		if (spriteRenderer) {
			spriteRenderer.color = colorToSet;
		}

		// For image
		else {
			image.color = colorToSet;
		}
	}
}
