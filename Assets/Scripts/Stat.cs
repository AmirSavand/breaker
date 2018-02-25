using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
	public Game game;

	public Text starsText;
	public Text highScoreText;

	public Text shipText;
	public Image shipModelImage;
	public Text shipHitpoints;
	public Text shipDamage;

	void OnEnable ()
	{
		// Set vars
		starsText.text = Storage.stars.ToString ();
		highScoreText.text = Storage.highScore.ToString ();

		// Set ship vars
		shipText.text = game.player.GetComponentInChildren<SpriteRenderer> ().sprite.name;
		shipModelImage.sprite = game.player.GetComponentInChildren<SpriteRenderer> ().sprite;
		shipHitpoints.text = game.player.GetComponent<Hitpoint> ().maxHitpoints.ToString ();
		shipDamage.text = game.player.fireDamage.ToString ();
	}
}
