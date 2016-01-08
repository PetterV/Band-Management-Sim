using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarDisplayPopSus : MonoBehaviour {

	public Image popBarImage;
	public Image susBarImage;

	private float pop;
	private float sus;
	private float maxPop;
	private float maxSus;

	private float greenAndBlueLevel;


	// Use this for initialization
	void Start () {
		popBarImage = GameObject.FindWithTag("popBar").GetComponent<Image>();
		susBarImage = GameObject.FindWithTag("susBar").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		pop = GameObject.FindWithTag("GameController").GetComponent<GameControl>().popularitet;
		maxPop = GameObject.FindWithTag("GameController").GetComponent<GameControl>().maxPopularitet;
		sus = GameObject.FindWithTag("GameController").GetComponent<GameControl>().publicSuspicion;
		maxSus = GameObject.FindWithTag("GameController").GetComponent<GameControl>().maxPublicSuspicion;


		popBarImage.fillAmount = pop / maxPop;

		float susToBeFilled = sus / maxSus;
		susBarImage.fillAmount = susToBeFilled;

		greenAndBlueLevel = 1 - susToBeFilled;

		susBarImage.color = new Color (1, greenAndBlueLevel, greenAndBlueLevel);
	}
}
